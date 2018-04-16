namespace ReTwitter.Services.External.Contracts
{
    public interface ITwitterApiCall
    {
        string GetTwitterData(string resourceUrl);
    }
}
