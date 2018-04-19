using System.Collections.Generic;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.DTO;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class UserFolloweeService : IUserFolloweeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFolloweeService followeeService;
        private readonly IUserTweetService userTweetService;

        public UserFolloweeService(IUnitOfWork unitOfWork, IFolloweeService followeeService, IUserTweetService userTweetService)
        {
            this.unitOfWork = unitOfWork;
            this.followeeService = followeeService;
            this.userTweetService = userTweetService;
        }

        public bool UserFolloweeExists(string userId, string followeeId)
        {
            bool exists = this.unitOfWork.UserFollowees.All.Any(a => a.UserId == userId && a.FolloweeId == followeeId);

            return exists;
        }

        public void SaveUserFollowees(string userId, IEnumerable<FolloweeFromApiDto> followees)
        {
            var userFolloweesToAdd = new List<UserFollowee>();

            foreach (var followee in followees)
            {
                if (!this.UserFolloweeExists(userId, followee.FolloweeId))
                {
                    var followeeToAddId = (
                        this.unitOfWork.Followees.All.FirstOrDefault(f => f.FolloweeId == followee.FolloweeId) ??
                        this.followeeService.Create(followee)).FolloweeId;
                    var userFolloweeToadd = new UserFollowee { UserId = userId, FolloweeId = followeeToAddId };
                    userFolloweesToAdd.Add(userFolloweeToadd);
                }
            }

            this.unitOfWork.UserFollowees.AddRange(userFolloweesToAdd);
            this.unitOfWork.SaveChanges();
        }

        public void SaveUserFollowee(string userId, FolloweeFromApiDto followee)
        {
            if (!this.UserFolloweeExists(userId, followee.FolloweeId))
            {
                var followeeToAddId = (
                    this.unitOfWork.Followees.All.FirstOrDefault(f => f.FolloweeId == followee.FolloweeId) ??
                    this.followeeService.Create(followee)).FolloweeId;
                var userFolloweeToadd = new UserFollowee { UserId = userId, FolloweeId = followeeToAddId };

                this.unitOfWork.UserFollowees.Add(userFolloweeToadd);
                this.unitOfWork.SaveChanges();
            }
        }

        public byte DeleteUserFollowee(string userId, string followeeId)
        {
            var userFolloweeFound =
                this.unitOfWork.UserFollowees.All.FirstOrDefault(w => w.FolloweeId == followeeId && w.UserId == userId);

            if (userFolloweeFound != null)
            {
                this.unitOfWork.UserFollowees.Delete(userFolloweeFound);
                this.unitOfWork.SaveChanges();
                foreach (var tweet in userFolloweeFound.Followee.TweetCollection)
                {
                    this.userTweetService.DeleteUserTweet(userId, tweet.TweetId);
                }

                if (!this.UsersSavedThisFolloweeById(followeeId))
                {
                    this.followeeService.Delete(followeeId);
                }
                return 1;
            }

            return 0;
        }

        public bool UsersSavedThisFolloweeById(string followeeId)
        {
            return this.unitOfWork.UserFollowees.All.Any(a => a.FolloweeId == followeeId);
        }
    }
}
