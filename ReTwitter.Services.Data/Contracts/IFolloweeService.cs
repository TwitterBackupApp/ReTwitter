using ReTwitter.DTO;
using System.Collections.Generic;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IFolloweeService
    {
        List<FolloweeDto> GetAllFollowees(string userId);

        FolloweeDto GetFolloweeById(string id);
    }
}
