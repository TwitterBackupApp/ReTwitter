using Newtonsoft.Json;

namespace ReTwitter.DTO.TwitterDto
{
    public class UserMentionDto
    {
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id_str")]
        public string FolloweeId { get; set; }
    }
}
