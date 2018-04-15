using System;
using System.Collections.Generic;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.DTO;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class TweetService : ITweetService
    {
        private readonly IMappingProvider mapper;
        private readonly IGenericRepository<Tweet> tweets;
        private readonly IGenericRepository<User> users;
        private readonly IUnitOfWork unitOfWork;

        public TweetService(IMappingProvider mapper, IGenericRepository<Tweet> tweets, IGenericRepository<User> users, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.tweets = tweets;
            this.users = users;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<TweetDto> GetTweetsByUserIdAndFolloweeId(string userId, string followeeId)
        {
            //var tweet = this.users.All
            //    .Include(i => i.FollowedPeople)
            //    .ThenInclude(ti => ti.Followee)
            //    .ThenInclude(ti => ti.TweetCollection)
            //    .Where(x => x.Id == id);

            var tweet = this.users.All
                .Where(x => x.Id == userId)
                .Select(s => new
                {
                    FollowedPeople = s.FollowedPeople.Where(w => w.Followee.FolloweeId == followeeId).Select(se => new
                    {
                        Tweets = se.Followee.TweetCollection.ToList()
                    })
                }).ToList();

            return this.mapper.ProjectTo<TweetDto>(tweet);
        }

        public TweetDto GetTweetByTweetId(string tweetId)
        {
            var tweet = this.tweets.All
                .FirstOrDefault(x => x.TweetId == tweetId);

            if (tweet == null)
            {
                throw new ArgumentException("Tweet with such ID is not found!");
            }

            return this.mapper.MapTo<TweetDto>(tweet);
        }

        public void Save(TweetDto dto)
        {
            var model = this.mapper.MapTo<Tweet>(dto);
            this.tweets.Add(model);
            this.unitOfWork.SaveChanges();
        }

        public void Delete(string id)
        {
            var tweet = this.tweets.All.FirstOrDefault(x => x.TweetId == id);

            if (tweet == null)
            {
                throw new ArgumentException("Tweet with such ID is not found!");
            }

            this.tweets.Delete(tweet);
            this.unitOfWork.SaveChanges();
        }
    }
}
