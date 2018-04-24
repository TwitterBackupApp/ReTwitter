namespace ReTwitter.Services.Data.Contracts
{
    public interface ICascadeDeleteService
    {
        void DeleteUserAndHisEntities(string userId);

        void DeleteUserFolloweeAndEntries(string followeeId, string userId);

        void DeleteEntitiesOfTweet(string tweetId);
    }
}
