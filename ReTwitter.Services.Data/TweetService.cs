﻿using System;
using System.Collections.Generic;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.DTO;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class TweetService : ITweetService
    {
        private readonly IMappingProvider mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITwitterApiCallService twitterApiCallService;
        private readonly ITweetTagService tweetTagService;
        private readonly ITagService tagService;
        private readonly IDateTimeParser dateTimeParser;

        public TweetService(IMappingProvider mapper, IUnitOfWork unitOfWork, ITwitterApiCallService twitterApiCallService, ITweetTagService tweetTagService, ITagService tagService, IDateTimeParser dateTimeParser)
        {
            if(mapper == null || unitOfWork == null || twitterApiCallService == null || 
                tweetTagService == null || tagService == null || dateTimeParser == null)
            {
                throw new ArgumentNullException();
            }

            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.twitterApiCallService = twitterApiCallService;
            this.tweetTagService = tweetTagService;
            this.tagService = tagService;
            this.dateTimeParser = dateTimeParser;
        }


        public TweetDto GetTweetByTweetId(string tweetId)
        {
            if(tweetId == null)
            {
                throw new ArgumentNullException("Tweet ID cannot be null!");
            }
            if(tweetId == string.Empty)
            {
                throw new ArgumentException("Tweet ID cannot be empty!");
            }

            var tweet = this.unitOfWork.Tweets.All
                .FirstOrDefault(x => x.TweetId == tweetId);

            if (tweet == null)
            {
                throw new ArgumentNullException("Tweet with such ID is not found!");
            }

            return this.mapper.MapTo<TweetDto>(tweet);
        }

        public void Save(TweetDto dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException("Tweet cannot be null!");
            }

            var model = this.mapper.MapTo<Tweet>(dto);
            this.unitOfWork.Tweets.Add(model);
            this.unitOfWork.SaveChanges();
        }

        public void Delete(string tweetId)
        {
            if (tweetId == null)
            {
                throw new ArgumentNullException("Tweet ID cannot be null!");
            }
            if (tweetId == string.Empty)
            {
                throw new ArgumentException("Tweet ID cannot be empty!");
            }

            var tweet = this.unitOfWork.Tweets.All.FirstOrDefault(x => x.TweetId == tweetId);

            if (tweet == null)
            {
                throw new ArgumentNullException("Tweet with such ID is not found!");
            }

            this.unitOfWork.Tweets.Delete(tweet);
            this.unitOfWork.SaveChanges();
        }

        public Tweet CreateFromApiDto(TweetFromApiDto tweet)
        {
            if (tweet == null)
            {
                throw new ArgumentNullException("Tweet cannot be null!");
            }

            var tweetToAdd = mapper.MapTo<Tweet>(tweet);
            this.unitOfWork.Tweets.Add(tweetToAdd);
            this.unitOfWork.SaveChanges();
            return tweetToAdd;
        }

        public Tweet CreateFromApiById(string tweetId)
        {
            if (tweetId == null)
            {
                throw new ArgumentNullException("Tweet ID cannot be null!");
            }
            if (tweetId == string.Empty)
            {
                throw new ArgumentException("Tweet ID cannot be empty!");
            }

            var tweet = this.twitterApiCallService.GetTweetByTweetId(tweetId);
            var tags = tweet.Entities.Hashtags;

            var tweetToAdd = new Tweet
            {
                FolloweeId = tweet.Followee.FolloweeId,
                OriginalTweetCreatedOn = this.dateTimeParser.ParseFromTwitter(tweet.OriginalTweetCreatedOn),
                TweetId = tweet.TweetId,
                Text = tweet.Text,
                UsersMentioned = tweet.Entities.UserMentions.Length
            };

            this.unitOfWork.Tweets.Add(tweetToAdd);
            this.unitOfWork.SaveChanges();

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    var tagFound = this.tagService.FindOrCreate(tag.Hashtag);
                    this.tweetTagService.AddTweetTagByTweetIdTagId(tagFound.Id, tweetId);
                }
            }

            return tweetToAdd;
        }

        public IEnumerable<TweetDto> GetTweetsByFolloweeIdAndUserId(string followeeId, string userId)
        {
            if(followeeId == null || userId == null)
            {
                throw new ArgumentNullException("Followee ID and User ID cannot be null!");
            }
            if(followeeId == "" || userId == "")
            {
                throw new ArgumentException("Followee ID and User ID cannot be empty!");
            }

            var tweets = this.unitOfWork
                .UserTweets
                .All
                .Where(w => w.UserId == userId && w.Tweet.FolloweeId == followeeId)
                .Select(se => se.Tweet)
                .ToList();

            var tweetDtos = tweets.Select(s => new TweetDto
            {
                TweetId = s.TweetId,
                OriginalTweetCreatedOn = s.OriginalTweetCreatedOn,
                UsersMentioned = s.UsersMentioned,
                Text = s.Text
            });

            return tweetDtos;
        }
    }
}
