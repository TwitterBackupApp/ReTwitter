using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReTwitter.Data.Models;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.External.Contracts;
using ReTwitter.Web.Models.TweetViewModel;
using System.Threading.Tasks;
using ReTwitter.Web.Areas.Admin.Models.Statistics;

namespace ReTwitter.Web.Controllers
{
    public class TweetController : Controller
    {
        private readonly ITwitterApiCaller twitterApiCall;
        private readonly ITwitterApiCallService twitterApiCallService;
        private readonly ITweetService tweetService;
        private readonly UserManager<User> manager;
        private readonly IUserTweetService userTweetService;
        private readonly ICascadeDeleteService cascadeDeleteService;

        public TweetController(ITwitterApiCallService twitterApiCallService, ITweetService tweetService, UserManager<User> manager, IUserTweetService userTweetService, ICascadeDeleteService cascadeDeleteService)
        {
            this.twitterApiCallService = twitterApiCallService;
            this.tweetService = tweetService;
            this.manager = manager;
            this.userTweetService = userTweetService;
            this.cascadeDeleteService = cascadeDeleteService;
        }

        public async Task<IActionResult> TweetDisplay(string followeeId)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.manager.GetUserAsync(HttpContext.User);
                var userId = user.Id;

                var savedTweets = this.tweetService.GetTweetsByFolloweeIdAndUserId(followeeId, userId);

                var vm = new TweetResultsViewModel { TweetResults = savedTweets };
                return View(vm);
            }

            return View();
        }

        // Await?? Since is asyncronous operation?
        public async Task<IActionResult> TweetSearchResult(string followeeId)
        {
            if (this.ModelState.IsValid)
            {
                var foundTweets = this.twitterApiCallService.GetTweetsByUserId(followeeId);

                var vm = new TweetSearchResultViewModel { TweetSearchResults = foundTweets };
                return View(vm);
            }

            return View();
        }

        public async Task<IActionResult> TweetAdd(string id)
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            bool tweetAlreadyAdded = this.userTweetService.UserTweetExists(userId, id);

            if (tweetAlreadyAdded)
            {
                return Json(false);
            }
            else
            {
                this.userTweetService.SaveSingleTweetToUserByTweetId(userId, id);

                return Json(true);
            }
        }

        public async Task<IActionResult> TweetDelete(string id)
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            this.userTweetService.DeleteUserTweet(userId, id);

            return View();
        }

        public IActionResult TweetAdminDelete(AdminDeleteTweetModel vm)
        {
            this.cascadeDeleteService.DeleteUserTweetAndEntities(vm.UserId, vm.TweetId);

            return Json(true);
        }
    }
}
