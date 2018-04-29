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
        private readonly ITwitterApiCallService twitterApiCallService;
        private readonly IDateTimeParser dateTimeParser;

        public FolloweeService(IUnitOfWork unitOfWork, IMappingProvider mapper, ITwitterApiCallService twitterApiCallService, IDateTimeParser dateTimeParser)
        {
            if(unitOfWork == null || mapper == null || twitterApiCallService == null || dateTimeParser == null)
            {
                throw new ArgumentNullException();
            }

            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.twitterApiCallService = twitterApiCallService;
            this.dateTimeParser = dateTimeParser;
        }


        public List<FolloweeDisplayListDto> GetAllFolloweesByUserId(string userId)
        {
            if(userId == null)
            {
                throw new ArgumentNullException("UserId cannot be null!");
            }
            if (userId == "")
            {
                throw new ArgumentException("UserId cannot be empty!");
            }

            var storedFollowees = this.unitOfWork
                .UserFollowees
                .All
                .Where(w => w.UserId == userId)
                .Select(s => new FolloweeDisplayListDto
                {
                    FolloweeId = s.Followee.FolloweeId,
                    Bio = s.Followee.Bio.Substring(0, 25) + "...",
                    FolloweeOriginallyCreatedOn = s.Followee.FolloweeOriginallyCreatedOn,
                    ScreenName = s.Followee.ScreenName,
                    Name = s.Followee.Name,
                })
                .ToList();

            return storedFollowees;
        }

        public FolloweeDto GetFolloweeById(string followeeId)
        {
            if(followeeId == null)
            {
                throw new ArgumentNullException("FolloweeId cannot be null!");
            }
            if(followeeId == "")
            {
                throw new ArgumentException("FolloweeId cannot be empty!");
            }

            var followee = this.unitOfWork
                .Followees
                .All
                .FirstOrDefault(x => x.FolloweeId == followeeId);

            return this.mapper.MapTo<FolloweeDto>(followee);
        }

        public Followee Create(FolloweeFromApiDto followee)
        {
            if(followee == null)
            {
                throw new ArgumentNullException("Followee cannot be null!");
            }

            Followee followeeToAdd = new Followee
            {
                FolloweeId = followee.FolloweeId,
                Bio = followee.Bio,
                ScreenName = followee.ScreenName,
                Name = followee.Name,
                FolloweeOriginallyCreatedOn = dateTimeParser.ParseFromTwitter(followee.FolloweeOriginallyCreatedOn),
                Url = followee.Url,
                FavoritesCount = followee.FavoritesCount,
                FollowersCount = followee.FollowersCount,
                FriendsCount = followee.FriendsCount,
                StatusesCount = followee.StatusesCount
            };
            
            this.unitOfWork.Followees.Add(followeeToAdd);
            this.unitOfWork.SaveChanges();
            return followeeToAdd;
        }

        public void Delete(string followeeId)
        {
            if(followeeId == null)
            {
                throw new ArgumentNullException("Followee ID cannot be null!");
            }
            if(followeeId == string.Empty)
            {
                throw new ArgumentException("Followee ID cannot be empty!");
            }

            var followeeFound = this.unitOfWork.Followees.All.FirstOrDefault(x => x.FolloweeId == followeeId);

            if (followeeFound == null)
            {
                throw new ArgumentNullException("Followee with such ID is not found!");
            }

            this.unitOfWork.Followees.Delete(followeeFound);
            this.unitOfWork.SaveChanges();
        }

        public void Update(string followeeId)
        {
            if (followeeId == null)
            {
                throw new ArgumentNullException("Followee ID cannot be null!");
            }
            if (followeeId == string.Empty)
            {
                throw new ArgumentException("Followee ID cannot be empty!");
            }

            var followeeToUpdate = this.unitOfWork.Followees.All.FirstOrDefault(fd => fd.FolloweeId == followeeId);

            if (followeeToUpdate == null)
            {
                throw new ArgumentNullException("Followee with such ID is not found!");
            }

            var updatedFollowee = this.twitterApiCallService.GetTwitterUserDetailsById(followeeId);

            followeeToUpdate.Bio = updatedFollowee.Bio;
            followeeToUpdate.FavoritesCount = updatedFollowee.FavoritesCount;
            followeeToUpdate.FollowersCount = updatedFollowee.FollowersCount;
            followeeToUpdate.FriendsCount = updatedFollowee.FriendsCount;
            followeeToUpdate.StatusesCount = updatedFollowee.StatusesCount;
            followeeToUpdate.Name = updatedFollowee.Name;
            followeeToUpdate.ScreenName = updatedFollowee.ScreenName;

            this.unitOfWork.Followees.Update(followeeToUpdate);
            this.unitOfWork.SaveChanges();
        }

        public bool FolloweeExistsInDatabase(string followeeId)
        {
            var exists = this.unitOfWork.Followees.AllAndDeleted.Any(a => a.FolloweeId == followeeId);

            return exists;
        }
    }
}