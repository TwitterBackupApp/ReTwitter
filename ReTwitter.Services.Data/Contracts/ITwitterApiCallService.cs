using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Services.Data.Contracts
{
    public interface ITwitterApiCallService
    {
        FolloweeDto[] GetTwitterUsersByScreenName(string name);
    }
}
