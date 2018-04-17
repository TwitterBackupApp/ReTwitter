using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Services.Data.Contracts
{
    public interface ITwitterApiCallService
    {
        FolloweeDto[] GetTwitterUsersByScreenName(string name);

        FolloweeDto GetTwitterUserDetailsById(string id);

        TweetDto[] GetTweetsByUserScreenName(string screenName);
    }
}
