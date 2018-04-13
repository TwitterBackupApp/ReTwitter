using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReTwitter.Data.Models;

namespace ReTwitter.Data.Configuration
{
    internal class TweetConfiguration : IEntityTypeConfiguration<Tweet>
    {
        public void Configure(EntityTypeBuilder<Tweet> builder)
        {
            //builder.Property(e => e.IsDeleted)
            //    .HasDefaultValue(false);
        }
    }
}
