using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Web.Models.SearchViewModels;

namespace ReTwitter.Web.Controllers
{
    [Authorize]
    public class SearchController: Controller
    {
        private readonly ITwitterApiCallService twitterApiCallService;

        public SearchController(ITwitterApiCallService twitterApiCallService)
        {
            this.twitterApiCallService = twitterApiCallService ?? throw new ArgumentNullException(nameof(twitterApiCallService));
        }


        public IActionResult Search()
        {            
            return this.View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchResult(SearchViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = new FolloweeFromApiDto[0];

                try
                {
                    result = twitterApiCallService.GetTwitterUsersByScreenName(model.SearchInput);
                }
                catch (Exception e)
                {
                    return this.View("NotFound");
                }

                if (result.Length < 1)
                {
                    TempData["Not-Found-Message"] = $"No results found matching {model.SearchInput}";
                    return RedirectToAction("Search");
                }
                var vm = new SearchResultsViewModel { SearchResults = result };

                return this.View(vm);
            }

            return this.View();
        }
    }
}
