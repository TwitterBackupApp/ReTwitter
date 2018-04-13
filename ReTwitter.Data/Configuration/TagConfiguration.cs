using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReTwitter.Data.Models;

namespace ReTwitter.Data.Configuration
{
    internal class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            //builder.HasMany(e => e.TweetTags)
            // .WithMany(a => a.AlbumTags)
            //     .HasForeignKey(e => e.AlbumId);
        }
    }
}
