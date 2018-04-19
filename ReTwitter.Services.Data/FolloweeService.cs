using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.DTO;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<FolloweeDisplayListDto> GetAllFollowees(string userId)
        {
            var storedFollowees = this.unitOfWork.UserFollowees.All
                                                 .Where(w => w.UserId == userId)
                                                 .Select(s => new FolloweeDisplayListDto
                                                 {
                                                     FolloweeId = s.Followee.FolloweeId,
                                                     Description = s.Followee.Description,
                                                     FolloweeOriginallyCreatedOn = s.Followee.FolloweeOriginallyCreatedOn,
                                                     ScreenName = s.Followee.ScreenName,
                                                     Name = s.Followee.Name
                                                 })
                                                 .ToList();
            return storedFollowees;
        }

        public FolloweeDto GetFolloweeById(string followeeId)
        {
            var followee = this.unitOfWork
                .Followees
                .All
                .FirstOrDefault(x => x.FolloweeId == followeeId);

            if (followee == null)
            {
                throw new ArgumentException("Followee not found!");
            }

            return this.mapper.MapTo<FolloweeDto>(followee);
        }

        public Followee Create(FolloweeDto followee)
        {
            var followeeToAdd = mapper.MapTo<Followee>(followee);
            this.unitOfWork.Followees.Add(followeeToAdd);
            this.unitOfWork.SaveChanges();
            return followeeToAdd;
        }
    }
}