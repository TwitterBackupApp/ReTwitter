using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            return this.unitOfWork.UserFollowees.AllAndDeleted.Any(a => a.FolloweeId == followeeId);
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
            else
            {
                var followeeToBeReadded =
                    this.unitOfWork.UserFollowees.AllAndDeleted.FirstOrDefault(a =>
                        a.FolloweeId == followee.FolloweeId && a.UserId == userId);

                if (followeeToBeReadded != null)
                {
                    followeeToBeReadded.IsDeleted = false;

                    this.unitOfWork.SaveChanges();
                }
            }
        }

        public byte DeleteUserFollowee(string userId, string followeeId)
        {
            var userFolloweeFound =
                this.unitOfWork.UserFollowees.All.FirstOrDefault(w => w.FolloweeId == followeeId && w.UserId == userId);

            if (userFolloweeFound != null)
            {
                var userTweetsToDelete =
                    this.unitOfWork.UserTweets.All.Where(w => w.Tweet.FolloweeId == followeeId && w.UserId == userId).Select(s => s.TweetId).ToList();

                if (userTweetsToDelete.Any())
                {
                    foreach (var tweetId in userTweetsToDelete)
                    {
                        this.userTweetService.DeleteUserTweet(userId, tweetId);
                    }
                }

                this.unitOfWork.UserFollowees.Delete(userFolloweeFound);
                this.unitOfWork.SaveChanges();

                if (!this.AnyUserSavedThisFolloweeById(followeeId))
                {
                    this.followeeService.Delete(followeeId);
                }
                return 1;
            }

            return 0;
        }

        public bool AnyUserSavedThisFolloweeById(string followeeId)
        {
              return this.unitOfWork.UserFollowees.All.Any(a => a.FolloweeId == followeeId);
        }
    }
}
