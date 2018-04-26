using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Web.Areas.Admin.Models.Statistics;

namespace ReTwitter.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrators")]
    public class StatisticsController : Controller
    {
        private readonly IFolloweeStatisticsService followeeStatisticsService;
        private readonly ITweetStatisticsService tweetStatisticsService;
        private readonly IUserStatisticsService userStatisticsService;

        public StatisticsController(IFolloweeStatisticsService followeeStatisticsService, ITweetStatisticsService tweetStatisticsService, IUserStatisticsService userStatisticsService)
        {
            this.followeeStatisticsService = followeeStatisticsService;
            this.tweetStatisticsService = tweetStatisticsService;
            this.userStatisticsService = userStatisticsService;
        }

        public IActionResult ShortStatistics(string userId, string userName)
        {
            var activelyFollowedAccountsCount = this.followeeStatisticsService.ActiveUserFolloweeCountByUserId(userId);
            var deletedAccountsCount = this.followeeStatisticsService.DeletedUserFolloweeCountByUserId(userId);
            var savedTweetsCount = this.tweetStatisticsService.SavedTweetsCountByUserId(userId);
            var deletedTweetsCount = this.tweetStatisticsService.DeletedTweetsCountByUserId(userId);

            var vm = new ShortStatisticsViewModel
            {
                ActivelyFollowedAccountsCount = activelyFollowedAccountsCount,
                DeletedAccountsCount = deletedAccountsCount,
                SavedTweetsCount = savedTweetsCount,
                DeletedTweetsCount = deletedTweetsCount,
                Username = userName
            };

            return this.View(vm);
        }

        public IActionResult TotalStatistics()
        {
            var activelyFollowedAccountsCount = this.followeeStatisticsService.ActiveFolloweeCount();
            var deletedAccountsCount = this.followeeStatisticsService.DeletedFolloweeCount();
            var savedTweetsCount = this.tweetStatisticsService.SavedTweetsCount();
            var deletedTweetsCount = this.tweetStatisticsService.DeletedTweetsCount();
            var activeUsersCount = this.userStatisticsService.ActiveUsersCount();
            var deletedUsersCount = this.userStatisticsService.DeletedUsersCount();

            var vm = new StatisticsViewModel
            {
                ActivelyFollowedAccountsCount = activelyFollowedAccountsCount,
                DeletedAccountsCount = deletedAccountsCount,
                SavedTweetsCount = savedTweetsCount,
                DeletedTweetsCount = deletedTweetsCount,
                ActiveUsers = activeUsersCount,
                DeletedUsers = deletedUsersCount
            };

            return this.View(vm);
        }
    }
}
