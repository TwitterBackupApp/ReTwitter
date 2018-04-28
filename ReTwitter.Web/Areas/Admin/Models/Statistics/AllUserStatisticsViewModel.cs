using System.Collections.Generic;
using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Web.Areas.Admin.Models.Statistics
{
    public class AllUserStatisticsViewModel
    {
        public IEnumerable<UserStatisticsModel> UserStatisticsModels { get; set; }
        public TotalStatisticsModel TotalStatistics { get; set; }
    }
}
