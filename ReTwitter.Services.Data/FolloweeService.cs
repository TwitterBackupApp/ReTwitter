﻿using ReTwitter.Data.Contracts;
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

        public List<FolloweeDto> GetAllFollowees(string userId)
        {
            var storedFollowees = this.unitOfWork.Users.All
                                                 .Where(w => w.Id == userId)
                                                 .Select(s => s.FollowedPeople)
                                                 .ToList(); //TODO REVIEW AND AMEND, IT RETURNS THE COLLECTION OF MANY-TO-MANY

            return this.mapper.ProjectTo<FolloweeDto>(storedFollowees).ToList();
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