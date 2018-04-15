using System.Collections.Generic;
using ReTwitter.DTO;

namespace ReTwitter.Services.Data.Contracts
{
    public interface ITweetService
    {
        IEnumerable<TweetDto> GetTweetsByUserIdAndFolloweeId(string userId, string followeeId);
        TweetDto GetTweetByTweetId(string tweetId);
        void Save(TweetDto dto);
        void Delete(string id);
    }
}
