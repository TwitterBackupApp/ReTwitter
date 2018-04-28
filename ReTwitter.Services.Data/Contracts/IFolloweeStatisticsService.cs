using System.Collections.Generic;
using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IFolloweeStatisticsService
    {
        int ActiveUserFolloweeCountByUserId(string userId);

        int DeletedUserFolloweeCountByUserId(string userId);

        int ActiveFolloweeCount();

        int DeletedFolloweeCount();

        IEnumerable<ActivelyFollowingModel> GetActiveFolloweesByUserId(string userId);

        IEnumerable<DeletedFolloweesModel> GetDeletedFolloweesByUserId(string userId);

        // IEnumerable<FolloweeStatisticsDto> AllAndDeletedFolloweesByUserId(string userId);
    }
}
