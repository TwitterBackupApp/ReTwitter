using System;
using Newtonsoft.Json;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data.TwitterApiService
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public T Deserialize<T>(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                throw new ArgumentNullException();
            }

            T objects = JsonConvert.DeserializeObject<T>(jsonString);

            return objects;
        }
    }
}
