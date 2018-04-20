using System;
using ReTwitter.Services.External.Contracts;

namespace ReTwitter.Services.External
{
    public class TwitterCredentialsProvider : ITwitterCredentialsProvider
    {
        public string ConsumerKey => Environment.GetEnvironmentVariable("ConsumerKey");

        public string ConsumerSecret => Environment.GetEnvironmentVariable("ConsumerSecret");

        public string AccessToken => Environment.GetEnvironmentVariable("AccessToken");

        public string AccessTokenSecret => Environment.GetEnvironmentVariable("AccessTokenSecret");
    }
}