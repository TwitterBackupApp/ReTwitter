using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Web.Models.TweetViewModel
{
    public class TweetResultsViewModel
    {
        public IEnumerable<TweetFromApiDto> TweetResults { get; set; }
    }
}
