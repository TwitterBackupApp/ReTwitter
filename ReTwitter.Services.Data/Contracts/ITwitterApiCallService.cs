using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Services.Data.Contracts
{
    public interface ITwitterApiCallService
    {
        UserDto[] GetTwitterUsersByScreenName(string name);
    }
}
