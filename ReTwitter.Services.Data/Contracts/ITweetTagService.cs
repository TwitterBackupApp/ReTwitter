namespace ReTwitter.Services.Data.Contracts
{
    public interface ITweetTagService
    {
        void DeleteTweetTag(int tagId, string tweetId);

        bool TweetsSavedThisTagById(int tagId);
    }
}
