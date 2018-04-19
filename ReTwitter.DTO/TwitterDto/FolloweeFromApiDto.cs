using Newtonsoft.Json;

namespace ReTwitter.DTO.TwitterDto
{
    public class FolloweeFromApiDto
    {
        [JsonProperty("id_str")]
        public string FolloweeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("followers_count")]
        public long FollowersCount { get; set; }

        [JsonProperty("friends_count")]
        public int FriendsCount { get; set; }

        [JsonProperty("created_at")]
        public string FolloweeOriginallyCreatedOn { get; set; }

        [JsonProperty("favourites_count")]
        public int FavoritesCount { get; set; }

        [JsonProperty("statuses_count")]
        public int StatusesCount { get; set; }
    }
}
