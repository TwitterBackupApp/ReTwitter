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
        // We need user context

        public FolloweeController(IFolloweeService followees)
        {
            this.followeeService = followees;
        }

        public ActionResult FolloweesCollection()
        {
            var followees = this.followeeService.GetAllFollowees();

            return View(followees);
        }

        public ActionResult FolloweeDetails()
        {
          //  var followees = this.followeeService.GetAllFollowees();

            return View();
        }
    }
}
