using ReTwitter.Data.Contracts;
using ReTwitter.DTO;
using ReTwitter.Infrastructure.Providers;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class FolloweeService : IFolloweeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMappingProvider mapper;

        public FolloweeService(IUnitOfWork unitOfWork, IMappingProvider mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public List<FolloweeDto> GetAllFollowees(string userId)
        {
            var storedFollowees = this.unitOfWork.Users.All
                                                 .Where(w => w.Id == userId)
                                                 .Select(s => s.FollowedPeople)
                                                 .ToList(); //TODO REVIEW AND AMEND, IT RETURNS THE COLLECTION OF MANY-TO-MANY

            return this.mapper.ProjectTo<FolloweeDto>(storedFollowees).ToList();
        }

        public FolloweeDto GetFolloweeById(string id)
        {
            var followee = this.unitOfWork
                .Followees
                .All
                .FirstOrDefault(x => x.FolloweeId == id);

            if (followee == null)
            {
                return null;
            }

            return this.mapper.MapTo<FolloweeDto>(followee);
        }
    }
}