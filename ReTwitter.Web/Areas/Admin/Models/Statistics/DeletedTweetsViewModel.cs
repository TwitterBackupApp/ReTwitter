using System.Collections.Generic;
using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Web.Areas.Admin.Models.Statistics
{
    public class DeletedTweetsViewModel
    {
        public IEnumerable<DeletedTweetsModel> DeletedTweetModels { get; set; }
    }
}
