using System;
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
            this.followeeService = followeeService ?? throw new ArgumentNullException(nameof(followeeService));
            this.twitterApiCallService = twitterApiCallService ?? throw new ArgumentNullException(nameof(twitterApiCallService));
            this.userFolloweeService = userFolloweeService ?? throw new ArgumentNullException(nameof(userFolloweeService));
            this.manager = manager ?? throw new ArgumentNullException(nameof(manager));
            this.cascadeDeleteService = cascadeDeleteService ?? throw new ArgumentNullException(nameof(cascadeDeleteService));
        }


        public async Task<IActionResult> FolloweeCollection()
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            var followeesToDisplay = this.followeeService.GetAllFolloweesByUserId(userId);

            return this.View(followeesToDisplay);
        }

        public IActionResult FolloweeDetails(string id)
        {
            FolloweeFromApiDto followee = new FolloweeFromApiDto();
            try
            {
                followee = this.twitterApiCallService.GetTwitterUserDetailsById(id);
            }
            catch (Exception e)
            {
                return this.View("NotFound");
            }


            if (followee != null)
            {
                return this.View(followee);
            }
            else
            {
                return this.View("NotFound");
            }
        }

        public IActionResult FolloweeDetailsFromDb(string id)
        {
            var followee = this.followeeService.GetFolloweeById(id);

            if (followee != null)
            {
                return this.View(followee);
            }
            else
            {
                return this.View("NotFound");
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
                return this.Json(false);
            }
            else
            {
                this.userFolloweeService.SaveUserFollowee(userId, followee);
                return this.Json(true);
            }
        }

        public async Task<IActionResult> FolloweeDeleted(string id)
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            this.cascadeDeleteService.DeleteUserFolloweeAndEntries(id, userId);

            return this.Json(true);
        }

        public IActionResult FolloweeUpdate(string followeeId)
        {
            this.followeeService.Update(followeeId);

            TempData["Success-Message"] = "Followee updated successfully!";

            return this.RedirectToAction("FolloweeCollection");
        }

        public IActionResult FolloweeAdminDelete(AdminDeleteFoloweeModel vm)
        {
            this.cascadeDeleteService.DeleteUserFolloweeAndEntries(vm.FolloweeId, vm.UserId);

            return this.Json(true);
        }
    }
}