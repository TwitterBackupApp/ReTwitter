using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;
using ReTwitter.DTO;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Web.Controllers
{
    public class FolloweeController: Controller
    {
        private readonly IFolloweeService followeeService;
        private readonly ITwitterApiCallService twitterApiCallService;
        private readonly IUserFolloweeService userFolloweeService;
        // We need user context

        public FolloweeController(IFolloweeService followees, ITwitterApiCallService twitterApiCallService, IUserFolloweeService userFolloweeService)
        {
            this.followeeService = followees;
            this.twitterApiCallService = twitterApiCallService;
            this.userFolloweeService = userFolloweeService;
        }

        public ActionResult FolloweeCollection()
        {
         //   var followees = this.followeeService.GetAllFollowees();

            return View();
        }

        public ActionResult FolloweeDetails(string id)
        {
            var followee = this.twitterApiCallService.GetTwitterUserDetailsById(id);

            return View(followee);
        }

        public ActionResult FolloweeAdded(string followeeId)
        {
            this.userFolloweeService.SaveUserFollowee("9b76c8f0-294b-4210-a0e8-563e3c226c7e", followeeId);

            return View();
        }
    }
}
