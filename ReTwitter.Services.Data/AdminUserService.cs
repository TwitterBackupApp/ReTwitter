using System;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReTwitter.Data.Contracts;
using ReTwitter.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class AdminUserService : IAdminUserService
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

        public void DeleteByUserId(string userId)
        {
            var user = this.unitOfWork.Users.All.FirstOrDefault(fd => fd.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("User not found!");
            }

            this.unitOfWork.Users.Delete(user);
            this.unitOfWork.SaveChanges();
        }
    }
}
