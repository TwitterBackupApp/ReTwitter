using ReTwitter.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IAdminUserService
    {
        Task<IEnumerable<UserDto>> AllAsync();
    }
}
