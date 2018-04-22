using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Services.Data.Contracts
{
    public interface ITwitterApiCallService
    {
        FolloweeFromApiDto[] GetTwitterUsersByScreenName(string name);

        FolloweeFromApiDto GetTwitterUserDetailsById(string id);

        TweetFromApiDto[] GetTweetsByUserScreenName(string screenName);

        TweetFromApiDto[] GetTweetsByUserId(string userId);

        TweetFromApiDto GetTweetByTweetId(string tweetId);
    }
}
