using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.DTO;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ReTwitter.Services.Data
{
    public class UserTweetService : IUserTweetService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITweetService tweetService;
        private readonly IMappingProvider mapper;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly ITweetTagService tweetTagService;

        public UserTweetService(IUnitOfWork unitOfWork, ITweetService tweetService, IMappingProvider mapper, IDateTimeProvider dateTimeProvider, ITweetTagService tweetTagService)
        {
            this.unitOfWork = unitOfWork;
            this.tweetService = tweetService;
            this.mapper = mapper;
            this.dateTimeProvider = dateTimeProvider;
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

        public void SaveSingleTweetToUserByTweetId(string userId, string tweetId)
        {
            var tweetToSaveToUser = this.unitOfWork.Tweets.AllAndDeleted.FirstOrDefault(w => w.TweetId == tweetId);

            if (tweetToSaveToUser == null) // if it's a new Tweet, it's a new UserTweet
            {
                tweetToSaveToUser = this.tweetService.CreateFromApiById(tweetId);
                var userTweetToadd = new UserTweet { UserId = userId, TweetId = tweetToSaveToUser.TweetId };

                this.unitOfWork.UserTweets.Add(userTweetToadd);
                this.unitOfWork.SaveChanges();
            }
            else
            {
                if (tweetToSaveToUser.IsDeleted)
                {
                    tweetToSaveToUser.IsDeleted = false;
                    tweetToSaveToUser.DeletedOn = null;
                    tweetToSaveToUser.ModifiedOn = this.dateTimeProvider.Now;

                    var tags = this.unitOfWork.TweetTags.AllAndDeleted.Where(w => w.TweetId == tweetId)
                        .Select(s => s.Tag).ToList();
                    foreach (var tag in tags)
                    {
                        var tweetTagToReAdd =
                            this.unitOfWork.TweetTags.AllAndDeleted.FirstOrDefault(
                                w => w.TagId == tag.Id && w.TweetId == tweetId);
                        if (tweetTagToReAdd != null && tweetTagToReAdd.IsDeleted)
                        {
                            tweetTagToReAdd.IsDeleted = false;
                            tweetTagToReAdd.DeletedOn = null;
                            tweetTagToReAdd.ModifiedOn = this.dateTimeProvider.Now;
                        }
                        if (tag.IsDeleted)
                        {
                            tag.IsDeleted = false;
                            tag.DeletedOn = null;
                            tag.ModifiedOn = this.dateTimeProvider.Now;
                        }
                    }
                    this.unitOfWork.SaveChanges();
                }

                if (!this.UserTweetExistsInDeleted(userId, tweetId))
                {
                    var userTweetToadd = new UserTweet { UserId = userId, TweetId = tweetId };

                    this.unitOfWork.UserTweets.Add(userTweetToadd);
                    this.unitOfWork.SaveChanges();
                }
                else
                {
                    var userTweetToBeReadded =
                        this.unitOfWork.UserTweets.AllAndDeleted.FirstOrDefault(a =>
                            a.TweetId == tweetId && a.UserId == userId);

                    if (userTweetToBeReadded != null)
                    {
                        userTweetToBeReadded.IsDeleted = false;
                        userTweetToBeReadded.DeletedOn = null;
                        userTweetToBeReadded.ModifiedOn = this.dateTimeProvider.Now;
                        this.unitOfWork.SaveChanges();
                    }
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
