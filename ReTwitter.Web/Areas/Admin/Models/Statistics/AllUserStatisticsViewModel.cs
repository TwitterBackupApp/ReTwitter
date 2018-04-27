using System.Collections.Generic;
using ReTwitter.DTO;

namespace ReTwitter.Web.Areas.Admin.Models.Statistics
{
    public class AllUserStatisticsViewModel
    {
        public IEnumerable<UserStatisticsModel> UserStatisticsModels { get; set; }
    }
}
