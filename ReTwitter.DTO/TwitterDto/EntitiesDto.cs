using Newtonsoft.Json;

namespace ReTwitter.DTO.TwitterDto
{
    public class EntitiesDto
    {
        [JsonProperty("hashtags")]
        public HashtagDto[] Hashtags { get; set; }

        [JsonProperty("user_mentions")]
        public UserMentionDto[] UserMentions { get; set; }
    }
}
