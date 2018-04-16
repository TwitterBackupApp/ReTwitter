using Newtonsoft.Json;

namespace ReTwitter.DTO.TwitterDto
{
    public class UserDto
    {
        [JsonProperty("id_str")]
        public string Id { get; set; }

        public string name { get; set; }
        public string screen_name { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string url { get; set; }
    }
}
