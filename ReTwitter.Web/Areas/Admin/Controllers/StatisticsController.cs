using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Web.Areas.Admin.Models.Statistics;

namespace ReTwitter.Web.Areas.Admin.Controllers
{
    using static WebConstants;

    [Area(AdminArea)]
    [Authorize(Roles = AdminRole)]
    public class StatisticsController : Controller
    {
        private readonly IFolloweeStatisticsService followeeStatisticsService;
        private readonly ITweetStatisticsService tweetStatisticsService;
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IFolloweeStatisticsService followeeStatisticsService, ITweetStatisticsService tweetStatisticsService, IStatisticsService statisticsService)
        {
            this.followeeStatisticsService = followeeStatisticsService ?? throw new ArgumentNullException(nameof(followeeStatisticsService));
            this.tweetStatisticsService = tweetStatisticsService ?? throw new ArgumentNullException(nameof(tweetStatisticsService));
            this.statisticsService = statisticsService ?? throw new ArgumentNullException(nameof(statisticsService));
        }

        public IActionResult Index()
        {
            var userStatisticsModels = this.statisticsService.UsersStatistics();

            var vm = new AllUserStatisticsViewModel { UserStatisticsModels = userStatisticsModels.UserStatisticsModels, TotalStatistics = userStatisticsModels.TotalStatisticsModel };

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
