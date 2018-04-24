using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.DTO;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class UserTweetService : IUserTweetService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITweetService tweetService;
        private readonly IMappingProvider mapper;

        public UserTweetService(IUnitOfWork unitOfWork, ITweetService tweetService, IMappingProvider mapper)
        {
            this.unitOfWork = unitOfWork;
            this.tweetService = tweetService;
            this.mapper = mapper;
        }

        public IEnumerable<TweetDto> GetTweetsByUserIdAndFolloweeId(string userId, string followeeId)
        {
            var tweets = this.unitOfWork.UserTweets.All
                .Where(x => x.UserId == userId && x.Tweet.FolloweeId == followeeId)
                .Select(s => s.Tweet).ToList();

            return this.mapper.ProjectTo<TweetDto>(tweets);
        }

        public bool UserTweetExists(string userId, string tweetId)
        {
            return this.unitOfWork.UserTweets
                .All
                .Any(a => a.UserId == userId && a.TweetId == tweetId);
        }

        public bool UserTweetExistsInDeleted(string userId, string tweetId)
        {
            return this.unitOfWork.UserTweets
                .AllAndDeleted
                .Any(a => a.UserId == userId && a.TweetId == tweetId);
        }

        public void SaveUserTweets(string userId, IEnumerable<TweetFromApiDto> tweets)
        {
            var userTweetsToAdd = new List<UserTweet>();

            foreach (var tweet in tweets)
            {
                if (!this.UserTweetExists(userId, tweet.TweetId))
                {
                    var tweetToAddId = (
                        this.unitOfWork.Tweets.All.FirstOrDefault(f => f.TweetId == tweet.TweetId) ??
                        this.tweetService.CreateFromApiDto(tweet)).TweetId;
                    var userTweetToadd = new UserTweet { UserId = userId, TweetId = tweetToAddId };
                    userTweetsToAdd.Add(userTweetToadd);
                }
            }

            this.unitOfWork.UserTweets.AddRange(userTweetsToAdd);
            this.unitOfWork.SaveChanges();
        }

        public void SaveSingleTweetToUserByTweetId(string userId, string tweetId)
        {
            if (!this.UserTweetExistsInDeleted(userId, tweetId))
            {
                var tweetToAddId = (
                    this.unitOfWork.Tweets.All.FirstOrDefault(f => f.TweetId == tweetId) ??
                    this.tweetService.CreateFromApiById(tweetId)).TweetId;
                var userTweetToadd = new UserTweet { UserId = userId, TweetId = tweetToAddId };

                this.unitOfWork.UserTweets.Add(userTweetToadd);
                this.unitOfWork.SaveChanges();
            }
            else
            {
                var tweetToBeReadded =
                    this.unitOfWork.UserTweets.AllAndDeleted.FirstOrDefault(a =>
                        a.TweetId == tweetId && a.UserId == userId);

                if (tweetToBeReadded != null)
                {
                    tweetToBeReadded.IsDeleted = false;

                    this.unitOfWork.SaveChanges();
                }
            }
        }

        public void DeleteUserTweet(string userId, string tweetId)
        {
            var userTweetFound = this.unitOfWork.UserTweets.All.FirstOrDefault(w => w.UserId == userId && w.TweetId == tweetId);

            if (userTweetFound != null)
            {
                this.unitOfWork.UserTweets.Delete(userTweetFound);
                this.unitOfWork.SaveChanges();
            }
        }

        public bool AnyUserSavedThisTweetById(string tweetId) => this.unitOfWork.UserTweets.All.Any(a => a.TweetId == tweetId);
    }
}
