﻿using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using System;

namespace ReTwitter.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ReTwitterDbContext context;
        private IGenericRepository<User> users;
        private IGenericRepository<Followee> followees;
        private IGenericRepository<Tag> tags;
        private IGenericRepository<Tweet> tweets;
        private IGenericRepository<UserFollowee> userFollowees;
        private IGenericRepository<UserTweet> userTweets;

        //public UnitOfWork(ReTwitterDbContext context)
        //{
        //    if (context == null)
        //    {
        //        throw new ArgumentNullException("Context should not be null");
        //    }
        //    this.context = context;
        //}

        public UnitOfWork(ReTwitterDbContext context, IGenericRepository<User> users, 
            IGenericRepository<Followee> followees, 
            IGenericRepository<Tag> tags, 
            IGenericRepository<Tweet> tweets,
            IGenericRepository<UserFollowee> userFollowees,
            IGenericRepository<UserTweet> userTweets)
        {
            this.context = context;
            this.users = users;
            this.followees = followees;
            this.tags = tags;
            this.tweets = tweets;
            this.userFollowees = userFollowees;
            this.userTweets = userTweets;
        }

        public IGenericRepository<User> Users
        {
            get
            {
                if (this.users == null)
                {
                    this.users = new GenericRepository<User>(context);
                }

                return this.users;
            }
        }

        public IGenericRepository<Followee> Followees
        {
            get
            {
                if (this.followees == null)
                {
                    this.followees = new GenericRepository<Followee>(context);
                }

                return this.followees;
            }
        }

        public IGenericRepository<Tag> Tags
        {
            get
            {
                if (this.tags == null)
                {
                    this.tags = new GenericRepository<Tag>(context);
                }

                return this.tags;
            }
        }

        public IGenericRepository<Tweet> Tweets
        {
            get
            {
                if (this.tweets == null)
                {
                    this.tweets = new GenericRepository<Tweet>(context);
                }

                return this.tweets;
            }
        }

        public IGenericRepository<UserFollowee> UserFollowees
        {
            get
            {
                if (this.userFollowees == null)
                {
                    this.userFollowees = new GenericRepository<UserFollowee>(context);
                }

                return this.userFollowees;
            }
        }

        public IGenericRepository<UserTweet> UserTweets
        {
            get
            {
                if (this.userTweets == null)
                {
                    this.userTweets = new GenericRepository<UserTweet>(context);
                }

                return this.userTweets;
            }
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
