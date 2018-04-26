using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReTwitter.Data.Models;
using ReTwitter.Services.Data.Contracts;
using System.Threading.Tasks;
using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Web.Controllers
{
    public class FolloweeController: Controller
    {
        private readonly IFolloweeService followeeService;
        private readonly ITwitterApiCallService twitterApiCallService;
        private readonly IUserFolloweeService userFolloweeService;
        private readonly UserManager<User> manager;
        private readonly ICascadeDeleteService cascadeDeleteService;

        public FolloweeController(IFolloweeService followeeService, 
            ITwitterApiCallService twitterApiCallService, IUserFolloweeService userFolloweeService, 
            UserManager<User> manager, ICascadeDeleteService cascadeDeleteService)
        {
            this.followeeService = followeeService;
            this.twitterApiCallService = twitterApiCallService;
            this.userFolloweeService = userFolloweeService;
            this.manager = manager;
            this.cascadeDeleteService = cascadeDeleteService;
        }


        public async Task<IActionResult> FolloweeCollection()
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            var followeesToDisplay = this.followeeService.GetAllFolloweesByUserId(userId);

            return View(followeesToDisplay);
        }

        public IActionResult FolloweeDetails(string id)
        {
            var followee = this.twitterApiCallService.GetTwitterUserDetailsById(id);

            return View(followee);
        }

        // Get followee from Db not from API
        public IActionResult FolloweeDetailsFromDb(string id)
        {
            var followee = this.followeeService.GetFolloweeById(id);
               // this.twitterApiCallService.GetTwitterUserDetailsById(id);

            return View(followee);
        }

        public async Task<IActionResult> FolloweeAdded(FolloweeFromApiDto followee)
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            bool followeeAlreadyExists = this.userFolloweeService.UserFolloweeExists(userId, followee.FolloweeId);

            if (followeeAlreadyExists)
            {
                return View("FolloweeAlreadyExists");
            }
            else
            {
                this.userFolloweeService.SaveUserFollowee(userId, followee);

                return View();
            }
        }

        public async Task<IActionResult> FolloweeDeleted(string followeeId)
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            this.cascadeDeleteService.DeleteUserFolloweeAndEntries(followeeId, userId);

            return View();
        }

        public IActionResult FolloweeUpdate(string followeeId)
        {
            this.followeeService.Update(followeeId);

            return RedirectToAction("FolloweeCollection");
        }
    }
}
