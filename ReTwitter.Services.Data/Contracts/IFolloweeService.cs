using ReTwitter.DTO;
using System.Collections.Generic;
using ReTwitter.Data.Models;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IFolloweeService
    {
        List<FolloweeDisplayListDto> GetAllFollowees(string userId);

        FolloweeDto GetFolloweeById(string followeeId);

        Followee Create(FolloweeDto followee);
    }
}
