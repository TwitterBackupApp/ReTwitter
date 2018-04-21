using System.Collections.Generic;
using ReTwitter.Data.Models;
using ReTwitter.DTO;
using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Services.Data.Contracts
{
    public interface ITweetService
    {
        TweetDto GetTweetByTweetId(string tweetId);

        void Save(TweetDto dto);

        void Delete(string id);

        Tweet Create(TweetFromApiDto tweet);

        IEnumerable<TweetDto> GetTweetsByFolloweeIdAndUserId(string followeeId, string userId);
    }
}
