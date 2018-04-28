using System.Collections.Generic;
using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Services.Data.Contracts
{
    public interface ITweetStatisticsService
    {
        IEnumerable<SavedTweetsModel> GetSavedTweetsByUserId(string userId);
        IEnumerable<DeletedTweetsModel> GetDeletedTweetsyUserId(string userId);
    }
}
