using System.Collections.Generic;
using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Web.Models.SearchViewModels
{
    public class SearchResultsViewModel
    {
        public IEnumerable<UserDto> SearchResults { get; set; }
    }
}
