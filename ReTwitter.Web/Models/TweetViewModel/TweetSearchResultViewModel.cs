using System.Collections.Generic;
using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Web.Models.TweetViewModel
{
    public class TweetSearchResultViewModel
    {
        public IEnumerable<TweetFromApiDto> TweetSearchResults { get; set; }
    }
}
