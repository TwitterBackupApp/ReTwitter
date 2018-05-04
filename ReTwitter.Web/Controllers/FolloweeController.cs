using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReTwitter.Data.Models;
using ReTwitter.Services.Data.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Web.Areas.Admin.Models.Statistics;

namespace ReTwitter.Web.Controllers
{
    [Authorize]
    public class FolloweeController : Controller
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

            if (followee != null)
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

        public async Task<IActionResult> FolloweeAdded(string id)
        {
            var followee = this.twitterApiCallService.GetTwitterUserDetailsById(id);

            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            bool followeeAlreadyExists = this.userFolloweeService.UserFolloweeExists(userId, followee.FolloweeId);

            if (followeeAlreadyExists)
            {
                return Json(true);
            }
            else
            {
                this.userFolloweeService.SaveUserFollowee(userId, followee);
                return Json(false);
            }
        }

        public async Task<IActionResult> FolloweeDeleted(string id)
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            this.cascadeDeleteService.DeleteUserFolloweeAndEntries(id, userId);

            return Json(true);
        }

        public IActionResult FolloweeUpdate(string followeeId)
        {
            this.followeeService.Update(followeeId);

            return RedirectToAction("FolloweeCollection");
        }

        public IActionResult FolloweeAdminDelete(AdminDeleteFoloweeModel vm)
        {
            this.cascadeDeleteService.DeleteUserFolloweeAndEntries(vm.FolloweeId, vm.UserId);

            return RedirectToAction("ActivelyFollowing", "Statistics", new { area = "Admin", userId = vm.UserId });
        }
    }
}