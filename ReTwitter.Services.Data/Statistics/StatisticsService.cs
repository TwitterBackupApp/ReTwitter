using System.Collections.Generic;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.DTO;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public StatisticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<UserStatisticsModel> UsersStatistics()
        {
            var allUsersTst = this.unitOfWork.Users.AllAndDeleted.ToList();

            var allUsers = this.unitOfWork.Users.AllAndDeleted.Select(s => new
            {
                Username = s.UserName,
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

            var usesStatisticsModels = new Dictionary<string, UserStatisticsModel>();

            foreach (var user in allUsers)
            {
                usesStatisticsModels[user.Username] = new UserStatisticsModel
                {
                    UserName = user.Username,
                    UserNameCreatedOn = user.CreatedOn,
                    ActiveStatus = user.DeletedStatus == true ? "Deleted" : "Active"
                };
            }

            foreach (var userModel in usesStatisticsModels)
            {
                userModel.Value.ActivelyFollowedAccountsCount = allUserFolloweeStatus.Count(w => w.UserName == userModel.Key && w.DeletedStatus == false);
                userModel.Value.DeletedAccountsCount = allUserFolloweeStatus.Count(w => w.UserName == userModel.Key && w.DeletedStatus == true);
                userModel.Value.SavedTweetsCount = allUserTweetsStatus.Count(w => w.UserName == userModel.Key && w.DeletedStatus == false);
                userModel.Value.DeletedTweetsCount = allUserTweetsStatus.Count(w => w.UserName == userModel.Key && w.DeletedStatus == true);
            }

            var statisticsModels = usesStatisticsModels.Values;

            return statisticsModels;
        }
    }
}
