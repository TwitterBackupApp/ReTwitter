namespace ReTwitter.Services.Data.Contracts
{
    public interface IUserStatisticsService
    {
        int ActiveUsersCount();
        int DeletedUsersCount();
    }
}
