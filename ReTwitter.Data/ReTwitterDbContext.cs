using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReTwitter.Data.Configuration;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Models.Abstracts;
using System;
using System.Linq;

namespace ReTwitter.Data
{
    public class ReTwitterDbContext : IdentityDbContext<User>, IReTwitterDbContext
    {
        public ReTwitterDbContext(DbContextOptions<ReTwitterDbContext> options)
            : base(options)
        {
        }

        public DbSet<Followee> Followees { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<TweetTag> TweetTags { get; set; }
        public DbSet<UserTweet> UserTweets { get; set; }
        public DbSet<UserFollowee> UserFollowees { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FolloweeConfiguration());
            builder.ApplyConfiguration(new TweetTagConfiguration());
            builder.ApplyConfiguration(new UserTweetConfiguration());
            builder.ApplyConfiguration(new UserFolloweeConfiguration());
            
            base.OnModelCreating(builder);
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}
