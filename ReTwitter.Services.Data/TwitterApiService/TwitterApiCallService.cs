using System;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.External.Contracts;

namespace ReTwitter.Services.Data.TwitterApiService
{
    public class TwitterApiCallService : ITwitterApiCallService
    {
        private readonly ITwitterApiCaller apiCaller;
        private readonly IJsonDeserializer jsonDeserializer;

        public TwitterApiCallService(ITwitterApiCaller apiCaller, IJsonDeserializer jsonDeserializer)
        {
            this.apiCaller = apiCaller ?? throw new ArgumentNullException(nameof(apiCaller));
            this.jsonDeserializer = jsonDeserializer ?? throw new ArgumentNullException(nameof(apiCaller));
        }

        public FolloweeFromApiDto[] GetTwitterUsersByScreenName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var searchString = "https://api.twitter.com/1.1/users/search.json?q=";
            var foundUsersString = this.apiCaller.GetTwitterData(searchString + name.Trim());
            var deserializedUsers = this.jsonDeserializer.Deserialize<FolloweeFromApiDto[]>(foundUsersString);
            return deserializedUsers;
        }

        public FolloweeFromApiDto GetTwitterUserDetailsById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var searchString = "https://api.twitter.com/1.1/users/show.json?user_id=";
            var foundUsersString = this.apiCaller.GetTwitterData(searchString + id.Trim());
            var deserializedUser = this.jsonDeserializer.Deserialize<FolloweeFromApiDto>(foundUsersString);
            return deserializedUser;
        }

        public TweetFromApiDto[] GetTweetsByUserScreenName(string screenName)
        {
            if (string.IsNullOrWhiteSpace(screenName))
            {
                throw new ArgumentNullException(nameof(screenName));
            }

            var link = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=" + screenName.Trim() +
                       "&count=100";
            var foundTweets = this.apiCaller.GetTwitterData(link);
            var deserializedTweet = this.jsonDeserializer.Deserialize<TweetFromApiDto[]>(foundTweets);

            return deserializedTweet;
        }

        public TweetFromApiDto[] GetTweetsByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var link = "https://api.twitter.com/1.1/statuses/user_timeline.json?user_id=" + userId.Trim() +
                       "&count=100";
            var foundTweets = this.apiCaller.GetTwitterData(link);
            var deserializedTweet = this.jsonDeserializer.Deserialize<TweetFromApiDto[]>(foundTweets);

            return deserializedTweet;
        }

        public TweetFromApiDto GetTweetByTweetId(string tweetId)
        {
            if (string.IsNullOrWhiteSpace(tweetId))
            {
                throw new ArgumentNullException(nameof(tweetId));
            }

            var link = "https://api.twitter.com/1.1/statuses/show.json?id=" + tweetId.Trim();
            var foundTweet = this.apiCaller.GetTwitterData(link);
            var deserializedTweet = this.jsonDeserializer.Deserialize<TweetFromApiDto>(foundTweet);

            return deserializedTweet;
        }
    }
}
