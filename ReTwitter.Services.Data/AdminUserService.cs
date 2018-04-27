using System;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReTwitter.Data.Contracts;
using ReTwitter.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReTwitter.Data.Models;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminUserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<UserDto>> AllAsync()
            => await this.unitOfWork
                .Users.All
                .ProjectTo<UserDto>()
                .ToListAsync();

        public async Task<User> SingleUserByUsernameAsync(string userName)
            => await this.unitOfWork
                .Users.All
                .FirstOrDefaultAsync(w => w.UserName == userName);
                

        public async Task<IEnumerable<UserDto>> AllWithoutMasterAdmins()
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
