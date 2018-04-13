using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReTwitter.Data.Models;

namespace ReTwitter.Data.Configuration
{
    internal class FolloweeConfiguration : IEntityTypeConfiguration<Followee>
    {
        public void Configure(EntityTypeBuilder<Followee> builder)
        {
            //builder.Property(e => e.IsDeleted)
            //    .HasDefaultValue(false);
        }
    }
}
