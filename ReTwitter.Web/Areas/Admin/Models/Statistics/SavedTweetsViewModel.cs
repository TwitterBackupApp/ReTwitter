using System.Collections.Generic;
using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Web.Areas.Admin.Models.Statistics
{
    public class SavedTweetsViewModel
    {
        public IEnumerable<SavedTweetsModel> SavedTweetModels { get; set; }
        public string UserId { get; set; }
    }
}
