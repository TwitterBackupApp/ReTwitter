using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReTwitter.Data.Models;
using ReTwitter.Services.Data.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Web.Controllers
{
    [Authorize]
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

            if(followee != null)
            {
                return View(followee);
            }
            else
            {
                return View("NotFound");
            }
        }

        public IActionResult FolloweeDetailsFromDb(string id)
        {
            var followee = this.followeeService.GetFolloweeById(id);

            if (followee != null)
            {
                return View(followee);
            }
            else
            {
                return View("NotFound");
            }
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

        public IActionResult FolloweeAdminDelete(string followeeId, string userId)
        {
            this.cascadeDeleteService.DeleteUserFolloweeAndEntries(followeeId, userId);

            return RedirectToAction("ActivelyFollowing", "Statistics", new {area = "Admin", userId = userId});
        }

    }
}
