using System;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.DTO;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data.Contracts;
using System.Collections.Generic;
using System.Linq;
using ReTwitter.DTO.TwitterDto;

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

        public List<FolloweeDisplayListDto> GetAllFolloweesByUserId(string userId)
        {
            var storedFollowees = this.unitOfWork.UserFollowees.All
                                                 .Where(w => w.UserId == userId)
                                                 .Select(s => new FolloweeDisplayListDto
                                                 {
                                                     FolloweeId = s.Followee.FolloweeId,
                                                     Description = s.Followee.Description.Substring(0, 15) + "...",
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

            return this.mapper.MapTo<FolloweeDto>(followee);
        }

        public Followee Create(FolloweeFromApiDto followee)
        {
            var followeeToAdd = mapper.MapTo<Followee>(followee);
            this.unitOfWork.Followees.Add(followeeToAdd);
            this.unitOfWork.SaveChanges();
            return followeeToAdd;
        }

        public void Delete(string followeeId)
        {
            var followeeFound = this.unitOfWork.Followees.All.FirstOrDefault(x => x.FolloweeId == followeeId);

            if (followeeFound == null)
            {
                throw new ArgumentException("Tag with such ID is not found!");
            }

            this.unitOfWork.Followees.Delete(followeeFound);
            this.unitOfWork.SaveChanges();
        }
    }
}