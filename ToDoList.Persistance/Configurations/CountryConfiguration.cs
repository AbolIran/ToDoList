using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Common;

namespace ToDoList.Persistance.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            // Table name
            builder.ToTable("Countries");

            // Primary key
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // Relationships
            builder.HasMany(c => c.Addresses)
                   .WithOne(a => a.Country)
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // BaseDomainEntity configuration
            builder.Property(c => c.DateCreated)
                   .IsRequired();

            builder.Property(c => c.LastModifiedDate)
                   .IsRequired();
        }
    }
}
