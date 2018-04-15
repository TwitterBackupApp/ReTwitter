using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReTwitter.Data.Contracts;
using ReTwitter.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class AdminUserService: IAdminUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMappingProvider mapper;

        public AdminUserService(IUnitOfWork unitOfWork, IMappingProvider mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> AllAsync()
            => await this.unitOfWork
                .Users.All
                .ProjectTo<UserDto>()
                .ToListAsync();
    }
}
