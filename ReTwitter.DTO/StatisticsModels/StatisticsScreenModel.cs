using System.Collections.Generic;

namespace ReTwitter.DTO.StatisticsModels
{
    public class StatisticsScreenModel
    {
        public IEnumerable<UserStatisticsModel> UserStatisticsModels { get; set; }
        public TotalStatisticsModel TotalStatisticsModel { get; set; }
    }
}
