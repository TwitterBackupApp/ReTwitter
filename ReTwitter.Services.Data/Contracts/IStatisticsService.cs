using System.Collections.Generic;
using ReTwitter.DTO;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IStatisticsService
    {
        IEnumerable<UserStatisticsModel> UsersStatistics();
    }
}
