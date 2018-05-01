using System;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;

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
        private IGenericRepository<TweetTag> tweetTags;

        public UnitOfWork(ReTwitterDbContext context, IGenericRepository<User> users, 
            IGenericRepository<Followee> followees, 
            IGenericRepository<Tag> tags, 
            IGenericRepository<Tweet> tweets,
            IGenericRepository<UserFollowee> userFollowees,
            IGenericRepository<UserTweet> userTweets,
            IGenericRepository<TweetTag> tweetTags)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.users = users ?? throw new ArgumentNullException(nameof(users));
            this.followees = followees ?? throw new ArgumentNullException(nameof(followees));
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
            this.tweets = tweets ?? throw new ArgumentNullException(nameof(tweets));
            this.userFollowees = userFollowees ?? throw new ArgumentNullException(nameof(userFollowees));
            this.userTweets = userTweets ?? throw new ArgumentNullException(nameof(userTweets));
            this.tweetTags = tweetTags ?? throw new ArgumentNullException(nameof(tweetTags));
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

        public IGenericRepository<TweetTag> TweetTags
        {
            get
            {
                if (this.tweetTags == null)
                {
                    this.tweetTags = new GenericRepository<TweetTag>(context);
                }

                return this.tweetTags;
            }
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
