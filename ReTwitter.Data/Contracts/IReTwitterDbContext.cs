using Microsoft.EntityFrameworkCore;
using ReTwitter.Data.Models;
using System;

namespace ReTwitter.Data.Contracts
{
    public interface IReTwitterDbContext: IDisposable
    {
        DbSet<Followee> Followees { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Tweet> Tweets { get; set; }
        DbSet<TweetTag> TweetTags { get; set; }
        DbSet<UserTweet> UserTweets { get; set; }
        DbSet<UserFollowee> UserFollowees { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}
