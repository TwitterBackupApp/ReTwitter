using System.Collections.Generic;
using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IFolloweeStatisticsService
    {
        IEnumerable<ActivelyFollowingModel> GetActiveFolloweesByUserId(string userId);

        IEnumerable<DeletedFolloweesModel> GetDeletedFolloweesByUserId(string userId);
    }
}
