using System.Collections.Generic;
using ReTwitter.DTO;

namespace ReTwitter.Web.Models.TweetViewModel
{
    public class TweetResultsViewModel
    {
        public IEnumerable<TweetDto> TweetResults { get; set; }
    }
}
