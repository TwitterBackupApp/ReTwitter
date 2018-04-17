using ReTwitter.DTO.TwitterDto;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.External.Contracts;

namespace ReTwitter.Services.Data.TwitterApiService
{
    public class TwitterApiCallService : ITwitterApiCallService
    {
        private readonly ITwitterApiCall apiCall;
        private readonly IJsonDeserializer jsonDeserializer;

        public TwitterApiCallService(ITwitterApiCall apiCall, IJsonDeserializer jsonDeserializer)
        {
            this.apiCall = apiCall;
            this.jsonDeserializer = jsonDeserializer;
        }

        public FolloweeDto[] GetTwitterUsersByScreenName(string name)
        {
            var searchString = "https://api.twitter.com/1.1/users/search.json?q=";
            var foundUsersString = apiCall.GetTwitterData(searchString + name.Trim());
            var deserializedUsers = this.jsonDeserializer.Deserialize<FolloweeDto[]>(foundUsersString);
            return deserializedUsers;
        }

        // Test method -  to be replaced via hashing
        public FolloweeDto GetTwitterUserDetailsById(string id)
        {
            var searchString = "https://api.twitter.com/1.1/users/show.json?user_id=";
            var foundUsersString = apiCall.GetTwitterData(searchString + id.Trim());       
            var deserializedUser = this.jsonDeserializer.Deserialize<FolloweeDto>(foundUsersString);
            return deserializedUser;
        }

        // Test method -  to be replaced via hashing
        public TweetDto[] GetTweetsByUserScreenName(string screenName)
        {
            var link = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=" + screenName +
                       "&count=100";
            var foundTweets = apiCall.GetTwitterData(link);
            var deserializedTweet = this.jsonDeserializer.Deserialize<TweetDto[]>(foundTweets);

            return deserializedTweet;
        }
    }
}
