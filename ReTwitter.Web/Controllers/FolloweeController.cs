using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Web.Controllers
{
    public class FolloweeController: Controller
    {
        private readonly IFolloweeService followeeService;
        private readonly ITwitterApiCallService twitterApiCallService;
        // We need user context

        public FolloweeController(IFolloweeService followees, ITwitterApiCallService twitterApiCallService)
        {
            this.followeeService = followees;
            this.twitterApiCallService = twitterApiCallService;
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
    }
}
