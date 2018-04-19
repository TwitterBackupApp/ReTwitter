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

        public FolloweeController(IFolloweeService followeeService, 
            ITwitterApiCallService twitterApiCallService, IUserFolloweeService userFolloweeService, 
            UserManager<User> manager)
        {
            this.followeeService = followeeService;
            this.twitterApiCallService = twitterApiCallService;
            this.userFolloweeService = userFolloweeService;
            this.manager = manager;
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

        public async Task<IActionResult> FolloweeAdded(string followeeId)
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;
            
            var followeeToAdd = this.twitterApiCallService.GetTwitterUserDetailsById(followeeId);

            this.userFolloweeService.SaveUserFollowee(userId, followeeToAdd);

            return View();
        }

        public async Task<IActionResult> FolloweeDeleted(string followeeId)
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            this.userFolloweeService.DeleteUserFollowee(userId, followeeId);
            return View();
        }
    }
}
