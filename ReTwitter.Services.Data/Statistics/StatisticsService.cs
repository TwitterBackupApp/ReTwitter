﻿using System;
using System.Collections.Generic;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.DTO.StatisticsModels;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public StatisticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public StatisticsScreenModel UsersStatistics()
        {
            var allUsers = this.unitOfWork.Users.AllAndDeleted.Select(s => new
            {
                Username = s.UserName,
                UserId = s.Id,
                CreatedOn = s.CreatedOn.Value,
                DeletedStatus = s.IsDeleted
            }).ToList();

            var allUserTweetsStatus = this.unitOfWork.UserTweets.AllAndDeleted.Select(s => new
            {
                UserName = s.User.UserName,
                DeletedStatus = s.IsDeleted
            }).ToList();

            var allUserFolloweeStatus = this.unitOfWork.UserFollowees.AllAndDeleted.Select(s => new
            {
                UserName = s.User.UserName,
                DeletedStatus = s.IsDeleted
            }).ToList();

            var usersStatisticsModels = new Dictionary<string, UserStatisticsModel>();
            var totalStatistics = new TotalStatisticsModel();

            foreach (var user in allUsers)
            {
                totalStatistics.TotalUsers++;
                if (user.DeletedStatus)
                {
                    totalStatistics.TotalDeletedUsers++;
                }
                else
                {
                    totalStatistics.TotalActiveUsers++;
                }
                usersStatisticsModels[user.Username] = new UserStatisticsModel
                {
                    UserName = user.Username,
                    UserId = user.UserId,
                    UserNameCreatedOn = user.CreatedOn,
                    ActiveStatus = user.DeletedStatus == true ? "Deleted" : "Active"
                };
            }

            foreach (var userModel in usersStatisticsModels)
            {
                userModel.Value.ActivelyFollowedAccountsCount = allUserFolloweeStatus.Count(w => w.UserName == userModel.Key && w.DeletedStatus == false);
                totalStatistics.TotalActivelyFollowedAccountsCount += userModel.Value.ActivelyFollowedAccountsCount;
                userModel.Value.DeletedAccountsCount = allUserFolloweeStatus.Count(w => w.UserName == userModel.Key && w.DeletedStatus == true);
                totalStatistics.TotalDeletedAccountsCount += userModel.Value.DeletedAccountsCount;
                userModel.Value.SavedTweetsCount = allUserTweetsStatus.Count(w => w.UserName == userModel.Key && w.DeletedStatus == false);
                totalStatistics.TotalSavedTweetsCount += userModel.Value.SavedTweetsCount;
                userModel.Value.DeletedTweetsCount = allUserTweetsStatus.Count(w => w.UserName == userModel.Key && w.DeletedStatus == true);
                totalStatistics.TotalDeletedTweetsCount += userModel.Value.DeletedTweetsCount;
            }

            var statisticsModels = new StatisticsScreenModel { TotalStatisticsModel = totalStatistics, UserStatisticsModels = usersStatisticsModels.Values };

            return statisticsModels;
        }
    }
}
