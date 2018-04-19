namespace ReTwitter.Services.External.Contracts
{
    public interface ITwitterApiCaller
    {
        string GetTwitterData(string resourceUrl);
    }
}
