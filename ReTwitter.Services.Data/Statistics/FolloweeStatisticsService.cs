using System;
using System.Collections.Generic;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.DTO.StatisticsModels;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data.Statistics
{
    public class FolloweeStatisticsService : IFolloweeStatisticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public FolloweeStatisticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IEnumerable<ActivelyFollowingModel> GetActiveFolloweesByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("UserId cannot be null");
            }

            var activeFollowees = this.unitOfWork.UserFollowees.All.Where(u => u.UserId == userId).Select(s =>
                new ActivelyFollowingModel
                {
                    FolloweeId = s.FolloweeId,
                    ScreenName = s.Followee.ScreenName,
                    Bio = s.Followee.Bio
                }).ToList();

            return activeFollowees;
        }

        public IEnumerable<DeletedFolloweesModel> GetDeletedFolloweesByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("UserId cannot be null");
            }
            var deletedeFollowees = this.unitOfWork.UserFollowees.AllAndDeleted.Where(u => u.UserId == userId && u.IsDeleted).Select(s =>
                new DeletedFolloweesModel
                {
                    ScreenName = s.Followee.ScreenName,
                    Bio = s.Followee.Bio,
                    DeletedOn = s.DeletedOn.Value
                }).ToList();

            return deletedeFollowees;
        }
    }
}
