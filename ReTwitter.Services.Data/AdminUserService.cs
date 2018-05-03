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
            if(unitOfWork == null)
            {
                throw new ArgumentNullException();
            }

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

        public async Task<User> SingleUserByIdAsync(string id)
            => await this.unitOfWork
                .Users.All
                .FirstOrDefaultAsync(w => w.Id == id);

        public async Task<IEnumerable<UserDto>> AllWithoutMasterAdmins()
            => await this.unitOfWork
                .Users.All
                .ProjectTo<UserDto>()
                .ToListAsync();

        public void DeleteByUserId(string userId)
        {
            if(userId == null)
            {
                throw new ArgumentNullException("User ID cannot be null!");
            }
            if(userId == string.Empty)
            {
                throw new ArgumentException("User ID cannot be empty!");
            }

            var user = this.unitOfWork.Users.All.FirstOrDefault(fd => fd.Id == userId);

            if (user == null)
            {
                throw new ArgumentNullException("User not found!");
            }

            this.unitOfWork.Users.Delete(user);
            this.unitOfWork.SaveChanges();
        }
    }
}
