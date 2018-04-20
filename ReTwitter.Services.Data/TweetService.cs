using System;
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

        public TweetService(IMappingProvider mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public TweetDto GetTweetByTweetId(string tweetId)
        {
            var tweet = this.unitOfWork.Tweets.All
                .FirstOrDefault(x => x.TweetId == tweetId);

            if (tweet == null)
            {
                throw new ArgumentException("Tweet with such ID is not found!");
            }

            return this.mapper.MapTo<TweetDto>(tweet);
        }

        public void Save(TweetDto dto)
        {
            var model = this.mapper.MapTo<Tweet>(dto);
            this.unitOfWork.Tweets.Add(model);
            this.unitOfWork.SaveChanges();
        }

        public void Delete(string tweetId)
        {
            var tweet = this.unitOfWork.Tweets.All.FirstOrDefault(x => x.TweetId == tweetId);

            if (tweet == null)
            {
                throw new ArgumentException("Tweet with such ID is not found!");
            }

            this.unitOfWork.Tweets.Delete(tweet);
            this.unitOfWork.SaveChanges();
        }

        public Tweet Create(TweetFromApiDto tweet)
        {
            var tweetToAdd = mapper.MapTo<Tweet>(tweet);
            this.unitOfWork.Tweets.Add(tweetToAdd);
            this.unitOfWork.SaveChanges();
            return tweetToAdd;
        }

        public IEnumerable<TweetDto> GetTweetsByFolloweeIdAndUserId(string followeeId, string userId)
        {
            var tweets = this.unitOfWork.UserTweets.All.Where(w => w.UserId == userId && w.Tweet.FolloweeId == followeeId).Select(se => se.Tweet).ToList();

            var tweetDtos = tweets.Select(s => new TweetDto
            {
                OriginalTweetCreatedOn = s.OriginalTweetCreatedOn,
                UsersMentioned = s.UsersMentioned.ToString(),
                Text = s.Text
            });

            return tweetDtos;
        }
    }
}
