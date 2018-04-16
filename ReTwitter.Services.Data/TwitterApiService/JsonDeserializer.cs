using Newtonsoft.Json;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data.TwitterApiService
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public T Deserialize<T>(string jsonString)
        {
            T objects = JsonConvert.DeserializeObject<T>(jsonString);

            return objects;
        }
    }
}
