using Microsoft.AspNetCore.Mvc;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.External.Contracts;
using ReTwitter.Web.Models.TweetViewModel;

namespace ReTwitter.Web.Controllers
{
    public class TweetController: Controller
    {
        private readonly ITwitterApiCaller twitterApiCall;
        private readonly ITwitterApiCallService twitterApiCallService;

        public TweetController(ITwitterApiCallService twitterApiCallService)
        {
            this.twitterApiCallService = twitterApiCallService;
        }

        public ActionResult TweetDisplay(string screenName)
        {
            if (this.ModelState.IsValid)
            {
                var hundredTweets = twitterApiCallService.GetTweetsByUserScreenName(screenName);

                var vm = new TweetResultsViewModel() { TweetResults = hundredTweets };


                return View(vm);
            }

            return View();          
        }
    }
}
