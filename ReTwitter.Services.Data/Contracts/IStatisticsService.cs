using System;
using System.Collections.Generic;
using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IStatisticsService
    {
        Tuple<IEnumerable<UserStatisticsModel>, TotalStatisticsModel> UsersStatistics();
    }
}
