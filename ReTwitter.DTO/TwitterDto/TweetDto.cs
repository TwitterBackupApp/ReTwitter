using Newtonsoft.Json;

namespace ReTwitter.DTO.TwitterDto
{
    public class TweetDto
    {
        [JsonProperty("id_str")]
        public string TweetId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("created_at")]
        public string OriginalTweetCreatedOn { get; set; }

        [JsonProperty("user")]
        public FolloweeDto Followee { get; set; }

        [JsonProperty("entities")]
        public EntitiesDto Entities { get; set; }
    }
}