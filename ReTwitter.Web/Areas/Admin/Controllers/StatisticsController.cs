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
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IFolloweeStatisticsService followeeStatisticsService, ITweetStatisticsService tweetStatisticsService, IStatisticsService statisticsService)
        {
            this.followeeStatisticsService = followeeStatisticsService;
            this.tweetStatisticsService = tweetStatisticsService;
            this.statisticsService = statisticsService;
        }

        public IActionResult Index()
        {
            var userStatisticsModels = this.statisticsService.UsersStatistics();

            var vm = new AllUserStatisticsViewModel { UserStatisticsModels = userStatisticsModels.Item1, TotalStatistics = userStatisticsModels.Item2 };

            return this.View(vm);
        }

        public IActionResult ActivelyFollowing(string userId)
        {
            var activelyFollowedAccounts = this.followeeStatisticsService.GetActiveFolloweesByUserId(userId);


            var vm = new ActivelyFollowingViewModel
            {
                ActivelyFollowingModels = activelyFollowedAccounts,
                UserId = userId
            };

            return this.View(vm);
        }

        public IActionResult DeletedFollowees(string userId)
        {
            var deletedFolloweeAccounts = this.followeeStatisticsService.GetDeletedFolloweesByUserId(userId);


            var vm = new DeletedFolloweesViewModel
            {
                DeletedFolloweesModels = deletedFolloweeAccounts
            };

            return this.View(vm);
        }

        public IActionResult SavedTweets(string userId)
        {
            var savedTweets = this.tweetStatisticsService.GetSavedTweetsByUserId(userId);


            var vm = new SavedTweetsViewModel
            {
                SavedTweetModels = savedTweets,
                UserId = userId
            };

            return this.View(vm);
        }

        public IActionResult DeletedTweets(string userId)
        {
            var deletedTweets = this.tweetStatisticsService.GetDeletedTweetsyUserId(userId);


            var vm = new DeletedTweetsViewModel
            {
                DeletedTweetModels = deletedTweets
            };

            return this.View(vm);
        }
    }
}
