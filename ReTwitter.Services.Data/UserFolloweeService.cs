using System.Collections.Generic;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.DTO;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class UserFolloweeService : IUserFolloweeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFolloweeService followeeService;

        public UserFolloweeService(IUnitOfWork unitOfWork, IFolloweeService followeeService)
        {
            this.unitOfWork = unitOfWork;
            this.followeeService = followeeService;
        }

        public bool UserFolloweeExists(string userId, string followeeId)
        {
            bool exists = this.unitOfWork.UserFollowees.All.Any(a => a.UserId == userId && a.FolloweeId == followeeId);

            return exists;
        }

        public void SaveUserFollowees(string userId, IEnumerable<FolloweeDto> followees)
        {
            var userFolloweesToAdd = new List<UserFollowee>();

            foreach (var followee in followees)
            {
                if (!this.UserFolloweeExists(userId, followee.FolloweeId))
                {
                    var followeeToAddId = (
                        this.unitOfWork.Followees.All.FirstOrDefault(f => f.FolloweeId == followee.FolloweeId) ??
                        this.followeeService.Create(followee)).FolloweeId;
                    var userFolloweeToadd = new UserFollowee {UserId = userId, FolloweeId = followeeToAddId};
                    userFolloweesToAdd.Add(userFolloweeToadd);
                }
            }

            this.unitOfWork.UserFollowees.AddRange(userFolloweesToAdd);
            this.unitOfWork.SaveChanges();
        }

        public void SaveUserFollowee(string userId, string followeeId)
        {
            var followee = this.followeeService.GetFolloweeById(followeeId);

            if (!this.UserFolloweeExists(userId, followeeId))
            {
                var followeeToAddId = (
                    this.unitOfWork.Followees.All.FirstOrDefault(f => f.FolloweeId == followee.FolloweeId) ??
                    this.followeeService.Create(followee)).FolloweeId;
                var userFolloweeToadd = new UserFollowee { UserId = userId, FolloweeId = followeeToAddId };
           
                this.unitOfWork.UserFollowees.Add(userFolloweeToadd);
                this.unitOfWork.SaveChanges();
            }
        }
    }
}
