namespace ReTwitter.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ReTwitter.Data.Models.Abstracts;

    internal class DataModelConfiguration : IEntityTypeConfiguration<DataModel>
    {
        public void Configure(EntityTypeBuilder<DataModel> builder)
        {
            builder.Property(p => p.IsDeleted)
                   .HasDefaultValue(false);
        }
    }
}
