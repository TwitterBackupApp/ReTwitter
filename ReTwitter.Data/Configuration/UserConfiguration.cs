using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReTwitter.Data.Models;

namespace ReTwitter.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.HasMany(m => m.FollowedPeople)
            //    .WithOne(o => o.User)
            //    .HasForeignKey(fk => fk.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
