using System.Collections.Generic;
using ReTwitter.DTO;
using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IUserTweetService
    {
        IEnumerable<TweetDto> GetTweetsByUserIdAndFolloweeId(string userId, string followeeId);
        bool UserTweetExists(string userId, string tweetId);
        void SaveUserTweets(string userId, IEnumerable<TweetFromApiDto> tweets);
        void DeleteUserTweet(string userId, string tweetTweetId);
        bool UsersSavedThisTweetById(string tweetId);
    }
}
