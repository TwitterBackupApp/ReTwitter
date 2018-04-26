using ReTwitter.DTO;
using System.Collections.Generic;
using ReTwitter.Data.Models;
using ReTwitter.DTO.TwitterDto;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IFolloweeService
    {
        List<FolloweeDisplayListDto> GetAllFolloweesByUserId(string userId);

        FolloweeDto GetFolloweeById(string followeeId);

        Followee Create(FolloweeFromApiDto followee);

        void Delete(string followeeId);

        void Update(string followeeId);

        bool FolloweeExistsInDatabase(string followeeId);
    }
}
