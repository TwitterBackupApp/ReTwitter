using Microsoft.AspNetCore.Mvc;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Web.Models.SearchViewModels;

namespace ReTwitter.Web.Controllers
{
    public class SearchController: Controller
    {
        private readonly ITwitterApiCallService twitterApiCallService;

        public SearchController(ITwitterApiCallService twitterApiCallService)
        {
            this.twitterApiCallService = twitterApiCallService;
        }


        public IActionResult Search()
        {            
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchResult(SearchViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = twitterApiCallService.GetTwitterUsersByScreenName(model.SearchInput);

                var vm = new SearchResultsViewModel { SearchResults = result };


                return View(vm);
            }

            TempData["Error-Message"] = "Please enter at least one character in order to use the search!";

            return View();
        }
    }
}
