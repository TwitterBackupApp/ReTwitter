using System;
using System.Threading.Tasks;
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
        private readonly IAdminUserService adminUserSevice;

        public StatisticsController(IFolloweeStatisticsService followeeStatisticsService, ITweetStatisticsService tweetStatisticsService, IStatisticsService statisticsService, IAdminUserService adminUserSevice)
        {
            this.followeeStatisticsService = followeeStatisticsService ?? throw new ArgumentNullException(nameof(followeeStatisticsService));
            this.tweetStatisticsService = tweetStatisticsService ?? throw new ArgumentNullException(nameof(tweetStatisticsService));
            this.statisticsService = statisticsService ?? throw new ArgumentNullException(nameof(statisticsService));
            this.adminUserSevice = adminUserSevice ?? throw new ArgumentNullException(nameof(adminUserSevice));
        }

        public IActionResult Index()
        {
            var userStatisticsModels = this.statisticsService.UsersStatistics();

            var vm = new AllUserStatisticsViewModel { UserStatisticsModels = userStatisticsModels.UserStatisticsModels, TotalStatistics = userStatisticsModels.TotalStatisticsModel };

            return this.View(vm);
        }

        public async Task<IActionResult> ActivelyFollowing(string userId)
        {
            var userExists = await this.adminUserSevice.UserExistsAsync(userId);

            if (!userExists)
            {
                return this.View("NotFound");
            }

            var activelyFollowedAccounts = this.followeeStatisticsService.GetActiveFolloweesByUserId(userId);

            var vm = new ActivelyFollowingViewModel
            {
                ActivelyFollowingModels = activelyFollowedAccounts,
                UserId = userId
            };
            return this.View(vm);
        }

        public async Task<IActionResult> DeletedFollowees(string userId)
        {
            var userExists = await this.adminUserSevice.UserExistsAsync(userId);

            if (!userExists)
            {
                return this.View("NotFound");
            }

            var deletedFolloweeAccounts = this.followeeStatisticsService.GetDeletedFolloweesByUserId(userId);

            var vm = new DeletedFolloweesViewModel
            {
                DeletedFolloweesModels = deletedFolloweeAccounts
            };
            return this.View(vm);
        }

        public async Task<IActionResult> SavedTweets(string userId)
        {
            var userExists = await this.adminUserSevice.UserExistsAsync(userId);

            if (!userExists)
            {
                return this.View("NotFound");
            }

            var savedTweets = this.tweetStatisticsService.GetSavedTweetsByUserId(userId);

            var vm = new SavedTweetsViewModel
            {
                SavedTweetModels = savedTweets,
                UserId = userId
            };
            return this.View(vm);
        }

        public async Task<IActionResult> DeletedTweets(string userId)
        {
            var userExists = await this.adminUserSevice.UserExistsAsync(userId);

            if (!userExists)
            {
                return this.View("NotFound");

            }
            var deletedTweets = this.tweetStatisticsService.GetDeletedTweetsyUserId(userId);

            var vm = new DeletedTweetsViewModel
            {
                DeletedTweetModels = deletedTweets
            };

            return this.View(vm);
        }
    }
}
