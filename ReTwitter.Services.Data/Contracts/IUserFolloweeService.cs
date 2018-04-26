using ReTwitter.DTO.TwitterDto;
using System.Collections.Generic;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IUserFolloweeService
    {
        bool UserFolloweeExists(string userId, string followeeId);

        void SaveUserFollowee(string userId, FolloweeFromApiDto followee);

        void DeleteUserFollowee(string userId, string followeeId);

        bool AnyUserSavedThisFolloweeById(string followeeId);
    }
}