using ReTwitter.DTO;
using System.Collections.Generic;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IUserTweetService
    {
        IEnumerable<TweetDto> GetTweetsByUserIdAndFolloweeId(string userId, string followeeId);

        bool UserTweetExists(string userId, string tweetId);

        void SaveSingleTweetToUserByTweetId(string userId, string tweetId);

        void DeleteUserTweet(string userId, string tweetTweetId);

        bool AnyUserSavedThisTweetById(string tweetId);
    }
}
