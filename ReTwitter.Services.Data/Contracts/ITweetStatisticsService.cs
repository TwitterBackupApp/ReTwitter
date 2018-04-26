namespace ReTwitter.Services.Data.Contracts
{
    public interface ITweetStatisticsService
    {
        int SavedTweetsCountByUserId(string userId);
        int DeletedTweetsCountByUserId(string userId);
        int SavedTweetsCount();
        int DeletedTweetsCount();
    }
}
