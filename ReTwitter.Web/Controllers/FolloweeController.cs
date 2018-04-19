using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReTwitter.Data.Models;
using ReTwitter.Services.Data.Contracts;
using System.Threading.Tasks;

namespace ReTwitter.Web.Controllers
{
    public class FolloweeController: Controller
    {
        private readonly IFolloweeService followeeService;
        private readonly ITwitterApiCallService twitterApiCallService;
        private readonly IUserFolloweeService userFolloweeService;
        private readonly UserManager<User> manager;

        public FolloweeController(IFolloweeService followeeService, 
            ITwitterApiCallService twitterApiCallService, IUserFolloweeService userFolloweeService, 
            UserManager<User> manager)
        {
            this.followeeService = followeeService;
            this.twitterApiCallService = twitterApiCallService;
            this.userFolloweeService = userFolloweeService;
            this.manager = manager;
        }


        public async Task<ActionResult> FolloweeCollection()
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            var followeesToDisplay = this.followeeService.GetAllFollowees(userId);

            return View(followeesToDisplay);
        }

        public ActionResult FolloweeDetails(string id)
        {
            var followee = this.twitterApiCallService.GetTwitterUserDetailsById(id);

            return View(followee);
        }

        public async Task<ActionResult> FolloweeAdded(string followeeId)
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            this.userFolloweeService.SaveUserFollowee(userId, followeeId);

            return View();
        }
    }
}
