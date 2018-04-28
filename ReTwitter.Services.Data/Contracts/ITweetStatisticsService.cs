using System.Collections.Generic;
using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Services.Data.Contracts
{
    public interface ITweetStatisticsService
    {
        int SavedTweetsCountByUserId(string userId);
        int DeletedTweetsCountByUserId(string userId);
        int SavedTweetsCount();
        int DeletedTweetsCount();
        IEnumerable<SavedTweetsModel> GetSavedTweetsByUserId(string userId);
        IEnumerable<DeletedTweetsModel> GetDeletedTweetsyUserId(string userId);
    }
}
