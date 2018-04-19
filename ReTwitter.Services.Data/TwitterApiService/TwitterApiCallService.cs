using ReTwitter.DTO.TwitterDto;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.External.Contracts;

namespace ReTwitter.Services.Data.TwitterApiService
{
    public class TwitterApiCallService : ITwitterApiCallService
    {
        private readonly ITwitterApiCaller _apiCaller;
        private readonly IJsonDeserializer jsonDeserializer;

        public TwitterApiCallService(ITwitterApiCaller apiCaller, IJsonDeserializer jsonDeserializer)
        {
            this._apiCaller = apiCaller;
            this.jsonDeserializer = jsonDeserializer;
        }

        public FolloweeFromApiDto[] GetTwitterUsersByScreenName(string name)
        {
            var searchString = "https://api.twitter.com/1.1/users/search.json?q=";
            var foundUsersString = _apiCaller.GetTwitterData(searchString + name.Trim());
            var deserializedUsers = this.jsonDeserializer.Deserialize<FolloweeFromApiDto[]>(foundUsersString);
            return deserializedUsers;
        }

        // Test method -  to be replaced via hashing
        public FolloweeFromApiDto GetTwitterUserDetailsById(string id)
        {
            var searchString = "https://api.twitter.com/1.1/users/show.json?user_id=";
            var foundUsersString = _apiCaller.GetTwitterData(searchString + id.Trim());       
            var deserializedUser = this.jsonDeserializer.Deserialize<FolloweeFromApiDto>(foundUsersString);
            return deserializedUser;
        }

        // Test method -  to be replaced via hashing
        public TweetFromApiDto[] GetTweetsByUserScreenName(string screenName)
        {
            var link = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=" + screenName.Trim() +
                       "&count=100";
            var foundTweets = _apiCaller.GetTwitterData(link);
            var deserializedTweet = this.jsonDeserializer.Deserialize<TweetFromApiDto[]>(foundTweets);

            return deserializedTweet;
        }
    }
}
