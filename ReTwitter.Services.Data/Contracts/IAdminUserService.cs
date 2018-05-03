using ReTwitter.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReTwitter.Data.Models;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IAdminUserService
    {
        Task<IEnumerable<UserDto>> AllAsync();
        Task<User> SingleUserByIdAsync(string id);
        Task<User> SingleUserByUsernameAsync(string userName);
        void DeleteByUserId(string userId);
    }
}
