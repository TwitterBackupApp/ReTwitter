using System;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Services.Data.Contracts;
using System.Linq;
using ReTwitter.Infrastructure.Providers;

namespace ReTwitter.Services.Data
{
    public class UserFolloweeService : IUserFolloweeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFolloweeService followeeService;
        private readonly IDateTimeProvider dateTimeProvider;

        public UserFolloweeService(IUnitOfWork unitOfWork, IFolloweeService followeeService, IDateTimeProvider dateTimeProvider)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.followeeService = followeeService ?? throw new ArgumentNullException(nameof(followeeService));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }


        public bool UserFolloweeExists(string userId, string followeeId)
        {
            if (string.IsNullOrWhiteSpace(followeeId))
            {
                throw new ArgumentNullException(nameof(followeeId));
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return this.unitOfWork.UserFollowees
                .All
                .Any(a => a.FolloweeId == followeeId && a.UserId == userId);
        }

        public virtual bool UserFolloweeExistsInDeleted(string userId, string followeeId)
        {
            if (string.IsNullOrWhiteSpace(followeeId))
            {
                throw new ArgumentNullException(nameof(followeeId));
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return this.unitOfWork.UserFollowees
                .AllAndDeleted
                .Any(a => a.FolloweeId == followeeId && a.UserId == userId);
        }

        public void SaveUserFollowee(string userId, FolloweeFromApiDto followee)
        {
            if (followee == null)
            {
                throw new ArgumentNullException(nameof(followee));
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var followeeToSaveToUser = this.unitOfWork.Followees.AllAndDeleted.FirstOrDefault(w => w.FolloweeId == followee.FolloweeId);

            if (followeeToSaveToUser == null) // if it's a new Followee, it's a new UserFollowee
            {
                followeeToSaveToUser = this.followeeService.Create(followee);
                var userFolloweeToadd = new UserFollowee { UserId = userId, FolloweeId = followeeToSaveToUser.FolloweeId };

                this.unitOfWork.UserFollowees.Add(userFolloweeToadd);
                this.unitOfWork.SaveChanges();
            }
            else
            {
                if (followeeToSaveToUser.IsDeleted)
                {
                    followeeToSaveToUser.IsDeleted = false;
                    followeeToSaveToUser.DeletedOn = null;
                    followeeToSaveToUser.ModifiedOn = this.dateTimeProvider.Now;
                    this.unitOfWork.SaveChanges();
                }

                if (!this.UserFolloweeExistsInDeleted(userId, followee.FolloweeId))
                {
                   var userFolloweeToadd = new UserFollowee { UserId = userId, FolloweeId = followee.FolloweeId };

                    this.unitOfWork.UserFollowees.Add(userFolloweeToadd);
                    this.unitOfWork.SaveChanges();
                }
                else
                {
                    var userFolloweeToBeReadded =
                        this.unitOfWork.UserFollowees.AllAndDeleted.FirstOrDefault(a =>
                            a.FolloweeId == followee.FolloweeId && a.UserId == userId);

                    if (userFolloweeToBeReadded != null)
                    {
                        userFolloweeToBeReadded.IsDeleted = false;
                        userFolloweeToBeReadded.DeletedOn = null;
                        userFolloweeToBeReadded.ModifiedOn = this.dateTimeProvider.Now;
                        this.unitOfWork.SaveChanges();
                    }
                }
            }
        }

        public void DeleteUserFollowee(string userId, string followeeId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(followeeId))
            {
                throw new ArgumentNullException(nameof(followeeId));
            }

            var userFolloweeFound =
                this.unitOfWork.UserFollowees.All.FirstOrDefault(w => w.FolloweeId == followeeId && w.UserId == userId);

            if (userFolloweeFound != null)
            {
                this.unitOfWork.UserFollowees.Delete(userFolloweeFound);
                this.unitOfWork.SaveChanges();
            }
        }

        public bool AnyUserSavedThisFolloweeById(string followeeId)
            => string.IsNullOrWhiteSpace(followeeId) 
                ? throw new ArgumentNullException(nameof(followeeId))
                : this.unitOfWork.UserFollowees.All.Any(a => a.FolloweeId == followeeId);
    }
}
