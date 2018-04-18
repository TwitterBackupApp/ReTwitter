using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReTwitter.Data.Models;

namespace ReTwitter.Data.Configuration
{
    internal class TweetTagConfiguration : IEntityTypeConfiguration<TweetTag>
    {
        public void Configure(EntityTypeBuilder<TweetTag> builder)
        {
            builder.HasKey(e => new { e.TweetId, e.TagId });

            builder.HasOne(e => e.Tweet)
                .WithMany(a => a.TweetTags)
                .HasForeignKey(e => e.TweetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Tag)
                .WithMany(a => a.TweetTags)
                .HasForeignKey(e => e.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Property(p => p.IsDeleted)
            //    .HasDefaultValue(false);
        }
    }
}
