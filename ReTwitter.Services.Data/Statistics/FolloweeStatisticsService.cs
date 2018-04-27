﻿using System.Collections.Generic;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.DTO;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data.Statistics
{
    public class FolloweeStatisticsService : IFolloweeStatisticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public FolloweeStatisticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public IEnumerable<ActivelyFollowingModel> GetActiveFolloweesByUserId(string userId)
        {
            var activeFollowees = this.unitOfWork.UserFollowees.All.Where(u => u.UserId == userId).Select(s =>
                new ActivelyFollowingModel
                {
                    FolloweeId = s.FolloweeId,
                    ScreenName = s.Followee.ScreenName,
                    Bio = s.Followee.Bio
                }).ToList();

            return activeFollowees;
        }

        public int ActiveUserFolloweeCountByUserId(string userId)
        {
            var activeUserFollowee = this.unitOfWork
                .UserFollowees
                .All
                .Count(w => w.UserId == userId);

            return activeUserFollowee;
        }

        public int ActiveFolloweeCount()
        {
            var activeFolloweesCount = this.unitOfWork
                .Followees
                .All
                .Count();

            return activeFolloweesCount;
        }

        public int DeletedUserFolloweeCountByUserId(string userId)
        {
            var deletedUserFollowee = this.unitOfWork
                                            .UserFollowees
                                            .AllAndDeleted
                                            .Count(w => w.UserId == userId && w.IsDeleted);

            return deletedUserFollowee;
        }

        public int DeletedFolloweeCount()
        {
            var activeFolloweesCount = this.unitOfWork
                .Followees
                .AllAndDeleted
                .Count(w => w.IsDeleted);

            return activeFolloweesCount;
        }

        //public IEnumerable<FolloweeStatisticsDto> AllAndDeletedFolloweesByUserId(string userId)
        //{
        //    var allFollowees = this.unitOfWork
        //                            .UserFollowees
        //                            .AllAndDeleted
        //                            .Where(w => w.UserId == userId)
        //                            .Select(s => new FolloweeStatisticsDto
        //                            {
        //                                Name = s.Followee.Name,
        //                                ScreenName = s.Followee.ScreenName,
        //                                IsDeleted = s.IsDeleted
        //                            })
        //                            .ToList();

        //    return allFollowees;
        //}
    }
}
