using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IStatisticsService
    {
        StatisticsScreenModel UsersStatistics();
    }
}
