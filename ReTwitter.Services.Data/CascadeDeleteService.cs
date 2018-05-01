using System;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class CascadeDeleteService : ICascadeDeleteService
    {
        private readonly IUserTweetService userTweetService;
        private readonly IUserFolloweeService userFolloweeService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IFolloweeService followeeService;
        private readonly ITweetService tweetService;
        private readonly ITweetTagService tweetTagService;
        private readonly IAdminUserService userService;

        public CascadeDeleteService(
                            IUserTweetService userTweetService,
                            IUserFolloweeService userFolloweeService,
                            IUnitOfWork unitOfWork,
                            IFolloweeService followeeService,
                            ITweetService tweetService,
                            ITweetTagService tweetTagService,
                            IAdminUserService userService
                            )
        {
            this.userTweetService = userTweetService ?? throw new ArgumentNullException(nameof(userTweetService));
            this.userFolloweeService = userFolloweeService ?? throw new ArgumentNullException(nameof(userFolloweeService));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.followeeService = followeeService ?? throw new ArgumentNullException(nameof(followeeService));
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
            this.tweetTagService = tweetTagService ?? throw new ArgumentNullException(nameof(tweetTagService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }


        public void DeleteUserAndHisEntities(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }
            var followeeIds = this.unitOfWork.UserFollowees.All
                                            .Where(w => w.UserId == userId)
                                            .Select(s => s.FolloweeId)
                                            .ToList();
            this.userService.DeleteByUserId(userId);

            if (followeeIds.Any())
            {
                foreach (var followeeId in followeeIds)
                {
                    this.DeleteUserFolloweeAndEntries(followeeId, userId);
                }
            }
        }

        public virtual void DeleteUserFolloweeAndEntries(string followeeId, string userId)
        {
            if (string.IsNullOrWhiteSpace(followeeId))
            {
                throw new ArgumentNullException(nameof(followeeId));
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            this.userFolloweeService.DeleteUserFollowee(userId, followeeId);

            if (!this.userFolloweeService.AnyUserSavedThisFolloweeById(followeeId))
            {
                this.followeeService.Delete(followeeId);
            }

            var tweetIds = this.unitOfWork.UserTweets
                .All
                .Where(w => w.Tweet.FolloweeId == followeeId && w.UserId == userId)
                .Select(s => s.TweetId)
                .ToList();

            if (tweetIds.Any())
            {
                foreach (var tweetId in tweetIds)
                {
                    this.DeleteUserTweetAndEntities(userId, tweetId);
                }
            }
        }

        public virtual void DeleteUserTweetAndEntities(string userId, string tweetId)
        {
            if (string.IsNullOrWhiteSpace(tweetId))
            {
                throw new ArgumentNullException(nameof(tweetId));
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            this.userTweetService.DeleteUserTweet(userId, tweetId);

            if (!this.userTweetService.AnyUserSavedThisTweetById(tweetId))
            {
                this.tweetService.Delete(tweetId);
                this.DeleteEntitiesOfTweet(tweetId);
            }
        }

        public virtual void DeleteEntitiesOfTweet(string tweetId)
        {
            if (string.IsNullOrWhiteSpace(tweetId))
            {
                throw new ArgumentNullException(nameof(tweetId));
            }

            var tagIds = this.unitOfWork.TweetTags
                .All
                .Where(w => w.TweetId == tweetId)
                .Select(s => s.TagId)
                .ToList();

            if (tagIds.Any())
            {
                foreach (var tagId in tagIds)
                {
                    this.tweetTagService.DeleteTweetTag(tagId, tweetId);
                }
            }
        }
    }
}