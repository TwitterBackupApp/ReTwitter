namespace ReTwitter.Services.Data.Contracts
{
    public interface ITweetTagService
    {
        void DeleteTweetTag(int tagId, string tweetId);

        bool AnyTweetSavedThisTagById(int tagId);
    }
}
