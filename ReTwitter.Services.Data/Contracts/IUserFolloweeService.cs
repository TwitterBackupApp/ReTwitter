using System.Collections.Generic;
using ReTwitter.DTO;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IUserFolloweeService
    {
        bool UserFolloweeExists(string userId, string followeeId);

        void SaveUserFollowees(string userId, IEnumerable<FolloweeDto> followees);

        void SaveUserFollowee(string userId, string followeeId);
    }
}
