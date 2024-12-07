using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Common;

namespace ToDoList.Persistance.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            // Table name
            builder.ToTable("Cities");

            // Primary key
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // Relationships
            builder.HasMany(c => c.Addresses)
                   .WithOne(a => a.City)
                   .HasForeignKey(a => a.CityId)
                   .OnDelete(DeleteBehavior.Restrict);

            // BaseDomainEntity configuration
            builder.Property(c => c.DateCreated)
                   .IsRequired();

            builder.Property(c => c.LastModifiedDate)
                   .IsRequired();
        }
    }
}
