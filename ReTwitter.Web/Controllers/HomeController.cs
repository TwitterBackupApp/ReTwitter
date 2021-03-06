﻿using Microsoft.AspNetCore.Mvc;
using ReTwitter.Web.Models;
using System.Diagnostics;

namespace ReTwitter.Web.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(CacheProfileName = "Hourly")]
        public IActionResult Index()
        {
            return this.View();
        }

        [ResponseCache(CacheProfileName = "Hourly")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        [ResponseCache(CacheProfileName = "Hourly")]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
