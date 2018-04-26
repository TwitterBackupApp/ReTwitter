namespace ReTwitter.Services.Data.Contracts
{
    public interface IFolloweeStatisticsService
    {
        int ActiveUserFolloweeCountByUserId(string userId);

        int DeletedUserFolloweeCountByUserId(string userId);

        int ActiveFolloweeCount();

        int DeletedFolloweeCount();

        // IEnumerable<FolloweeStatisticsDto> AllAndDeletedFolloweesByUserId(string userId);
    }
}
