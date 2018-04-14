using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReTwitter.Data.Models;

namespace ReTwitter.Data.Configuration
{
    internal class UserFolloweeConfiguration : IEntityTypeConfiguration<UserFollowee>
    {
        public void Configure(EntityTypeBuilder<UserFollowee> builder)
        {
            builder.HasKey(e => new { e.FolloweeId, e.UserId });

            builder.HasOne(e => e.Followee)
                .WithMany(a => a.InUsersFavorites)
                .HasForeignKey(e => e.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.User)
                .WithMany(a => a.FollowedPeople)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
