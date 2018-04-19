using System.Collections.Generic;
using ReTwitter.DTO;
using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IUserFolloweeService
    {
        bool UserFolloweeExists(string userId, string followeeId);

        void SaveUserFollowees(string userId, IEnumerable<FolloweeFromApiDto> followees);

        void SaveUserFollowee(string userId, FolloweeFromApiDto followee);

        byte DeleteUserFollowee(string userId, string followeeId);
    }
}
