using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReTwitter.Data.Models;

namespace ReTwitter.Data.Configuration
{
    internal class TweetUserMentionConfiguration : IEntityTypeConfiguration<TweetUserMention>
    {
        public void Configure(EntityTypeBuilder<TweetUserMention> builder)
        {
            builder.HasKey(e => new { e.FolloweeId, e.TweetId });

            builder.HasOne(e => e.Followee)
                .WithMany(a => a.MentionedInTweets)
                .HasForeignKey(e => e.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(e => e.Tweet)
                .WithMany(a => a.UsersMentioned)
                .HasForeignKey(e => e.TweetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
