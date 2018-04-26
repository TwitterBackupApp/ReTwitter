namespace ReTwitter.Services.Data.Contracts
{
    public interface ICascadeDeleteService
    {
        void DeleteUserAndHisEntities(string userId);

        void DeleteUserFolloweeAndEntries(string followeeId, string userId);

        void DeleteUserTweetAndEntities(string userId, string tweetId);

        void DeleteEntitiesOfTweet(string tweetId);
    }
}
