using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReTwitter.Data.Models;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Web.Models.TweetViewModel;
using System.Threading.Tasks;
using ReTwitter.Web.Areas.Admin.Models.Statistics;

namespace ReTwitter.Web.Controllers
{
    public class TweetController : Controller
    {
        private readonly ITwitterApiCallService twitterApiCallService;
        private readonly ITweetService tweetService;
        private readonly IFolloweeService followeeService;
        private readonly UserManager<User> manager;
        private readonly IUserTweetService userTweetService;
        private readonly ICascadeDeleteService cascadeDeleteService;

        public TweetController(ITwitterApiCallService twitterApiCallService, ITweetService tweetService, UserManager<User> manager, IUserTweetService userTweetService, ICascadeDeleteService cascadeDeleteService, IFolloweeService followeeService)
        {
            this.twitterApiCallService = twitterApiCallService ?? throw new ArgumentNullException(nameof(twitterApiCallService));
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
            this.manager = manager ?? throw new ArgumentNullException(nameof(manager));
            this.userTweetService = userTweetService ?? throw new ArgumentNullException(nameof(userTweetService));
            this.cascadeDeleteService = cascadeDeleteService ?? throw new ArgumentNullException(nameof(cascadeDeleteService));
            this.followeeService = followeeService ?? throw new ArgumentNullException(nameof(followeeService));
        }

        public async Task<IActionResult> TweetDisplay(string followeeId)
        {
            if (string.IsNullOrWhiteSpace(followeeId))
            {
                return this.View("NotFound");
            }

            var followeeExists = this.followeeService.FolloweeExistsInDatabase(followeeId);

            if (!followeeExists)
            {
                return this.View("NotFound");
            }

            var user = await this.manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            var savedTweets = this.tweetService.GetTweetsByFolloweeIdAndUserId(followeeId, userId);

            var vm = new TweetResultsViewModel { TweetResults = savedTweets };
            return this.View(vm);
        }

        public IActionResult TweetSearchResult(string followeeId)
        {
            if (this.ModelState.IsValid)
            {
                var foundTweets = this.twitterApiCallService.GetTweetsByUserId(followeeId);

                var vm = new TweetSearchResultViewModel { TweetSearchResults = foundTweets };
                return this.View(vm);
            }

            return this.View();
        }

        public async Task<IActionResult> TweetAdd(string id)
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            bool tweetAlreadyAdded = this.userTweetService.UserTweetExists(userId, id);

            if (tweetAlreadyAdded)
            {
                return this.Json(false);
            }
            else
            {
                this.userTweetService.SaveSingleTweetToUserByTweetId(userId, id);

                return this.Json(true);
            }
        }

        public async Task<IActionResult> TweetDelete(string id)
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            this.userTweetService.DeleteUserTweet(userId, id);

            return this.Json(true);
        }

        public IActionResult TweetAdminDelete(AdminDeleteTweetModel vm)
        {
            this.cascadeDeleteService.DeleteUserTweetAndEntities(vm.UserId, vm.TweetId);

            return this.Json(true);
        }
    }
}
