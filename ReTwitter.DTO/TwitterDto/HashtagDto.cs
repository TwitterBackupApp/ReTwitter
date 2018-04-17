using Newtonsoft.Json;

namespace ReTwitter.DTO.TwitterDto
{
    public class HashtagDto
    {
        [JsonProperty("text")]
        public string Hashtag { get; set; }
    }
}
