using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReTwitter.Data.Models;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.External.Contracts;
using ReTwitter.Web.Models.TweetViewModel;

namespace ReTwitter.Web.Controllers
{
    public class TweetController: Controller
    {
        private readonly ITwitterApiCaller twitterApiCall;
        private readonly ITwitterApiCallService twitterApiCallService;
        private readonly ITweetService tweetService;
        private readonly UserManager<User> manager;

        public TweetController(ITwitterApiCallService twitterApiCallService, ITweetService tweetService, UserManager<User> manager)
        {
            this.twitterApiCallService = twitterApiCallService;
            this.tweetService = tweetService;
            this.manager = manager;
        }

        public async Task<IActionResult> TweetDisplay(string followeeId)
        {
            if (this.ModelState.IsValid)
            {
                //var hundredTweets = twitterApiCallService.GetTweetsByUserScreenName(screenName);

                //var vm = new TweetResultsViewModel() { TweetResults = hundredTweets };

                var user = await this.manager.GetUserAsync(HttpContext.User);
                var userId = user.Id;

                var savedTweets = this.tweetService.GetTweetsByFolloweeIdAndUserId(followeeId, userId);

                var vm = new TweetResultsViewModel { TweetResults = savedTweets };
                return View(vm);
            }

            return View();          
        }
    }
}
