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
    }
}
