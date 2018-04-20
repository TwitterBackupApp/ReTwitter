namespace ReTwitter.Services.External.Contracts
{
    public interface ITwitterCredentialsProvider
    {
        string ConsumerKey { get; }

        string ConsumerSecret { get; }

        string AccessToken { get; }

        string AccessTokenSecret { get; }
    }
}
