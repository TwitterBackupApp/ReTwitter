using ReTwitter.DTO;
using System.Collections.Generic;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IFolloweeService
    {
        List<FolloweeDto> GetAllFollowees();

        FolloweeDto GetFolloweeById(string id);
    }
}
