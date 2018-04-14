using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReTwitter.Data.Configuration;
using ReTwitter.Data.Models;
using ReTwitter.Data.Models.Abstracts;

namespace ReTwitter.Data
{
    public class ReTwitterDbContext : IdentityDbContext<User>
    {
        public ReTwitterDbContext(DbContextOptions<ReTwitterDbContext> options)
            : base(options)
        {
        }

        public DbSet<Followee> Followees { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<TweetTag> TweetTags { get; set; }
        public DbSet<TweetUserMention> TweetUserMentions { get; set; }
        public DbSet<UserFollowee> UserFollowees { get; set; }


        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DataModelConfiguration());
            builder.ApplyConfiguration(new FolloweeConfiguration());
            //builder.ApplyConfiguration(new TagConfiguration());
           // builder.ApplyConfiguration(new TweetConfiguration());
            builder.ApplyConfiguration(new TweetTagConfiguration());
            builder.ApplyConfiguration(new TweetUserMentionConfiguration());
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
    }
}
