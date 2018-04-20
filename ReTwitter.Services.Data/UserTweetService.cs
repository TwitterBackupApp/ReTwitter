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
        private readonly ITweetTagService tweetTagService;

        public UserTweetService(IUnitOfWork unitOfWork, ITweetService tweetService, IMappingProvider mapper, ITweetTagService tweetTagService)
        {
            this.unitOfWork = unitOfWork;
            this.tweetService = tweetService;
            this.mapper = mapper;
            this.tweetTagService = tweetTagService;
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
            bool exists = this.unitOfWork.UserTweets.All.Any(a => a.UserId == userId && a.TweetId == tweetId);

            return exists;
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
                        this.tweetService.Create(tweet)).TweetId;
                    var userTweetToadd = new UserTweet { UserId = userId, TweetId = tweetToAddId };
                    userTweetsToAdd.Add(userTweetToadd);
                }
            }

            this.unitOfWork.UserTweets.AddRange(userTweetsToAdd);
            this.unitOfWork.SaveChanges();
        }

        public void DeleteUserTweet(string userId, string tweetId)
        {
            var userTweetFound = this.unitOfWork.UserTweets.All.FirstOrDefault(w => w.UserId == userId && w.TweetId == tweetId);

            if (userTweetFound != null)
            {
                this.unitOfWork.UserTweets.Delete(userTweetFound);
                this.unitOfWork.SaveChanges();

                if (!this.AnyUserSavedThisTweetById(tweetId))
                {
                    var tweetTagsToDelete =
                        this.unitOfWork.TweetTags.All.Where(w => w.TweetId == tweetId).Select(s => s.TagId).ToList();

                    if (tweetTagsToDelete.Any())
                    {
                        foreach (var tagId in tweetTagsToDelete)
                        {
                            this.tweetTagService.DeleteTweetTag(tagId, tweetId);
                        }
                    }

                    this.tweetService.Delete(tweetId);
                }
            }
        }

        public bool AnyUserSavedThisTweetById(string tweetId)
        {
            return this.unitOfWork.UserTweets.All.Any(a => a.TweetId == tweetId);
        }
    }
}
