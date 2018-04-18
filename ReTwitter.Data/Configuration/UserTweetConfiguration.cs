using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReTwitter.Data.Models;

namespace ReTwitter.Data.Configuration
{
    internal class UserTweetConfiguration : IEntityTypeConfiguration<UserTweet>
    {
        public void Configure(EntityTypeBuilder<UserTweet> builder)
        {
            builder.HasKey(e => new { e.UserId, e.TweetId });

            builder.HasOne(e => e.User)
                .WithMany(a => a.TweetCollection)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(e => e.Tweet)
                .WithMany(a => a.UserTweetCollection)
                .HasForeignKey(e => e.TweetId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Property(p => p.IsDeleted)
            //    .HasDefaultValue(false);
        }
    }
}
