using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Services.Data.Contracts;
using System.Linq;

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
            return this.unitOfWork.UserFollowees
                .All
                .Any(a => a.FolloweeId == followeeId && a.UserId == userId);
        }

        public bool UserFolloweeExistsInDeleted(string userId, string followeeId)
        {
            return this.unitOfWork.UserFollowees
                .AllAndDeleted
                .Any(a => a.FolloweeId == followeeId && a.UserId == userId);
        }

        public void SaveUserFollowee(string userId, FolloweeFromApiDto followee)
        {
            if (!this.UserFolloweeExistsInDeleted(userId, followee.FolloweeId))
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

        public void DeleteUserFollowee(string userId, string followeeId)
        {
            var userFolloweeFound =
                this.unitOfWork.UserFollowees.All.FirstOrDefault(w => w.FolloweeId == followeeId && w.UserId == userId);

            if (userFolloweeFound != null)
            {
                this.unitOfWork.UserFollowees.Delete(userFolloweeFound);
                this.unitOfWork.SaveChanges();
            }
        }

        public bool AnyUserSavedThisFolloweeById(string followeeId) => this.unitOfWork.UserFollowees.All.Any(a => a.FolloweeId == followeeId);
    }
}
