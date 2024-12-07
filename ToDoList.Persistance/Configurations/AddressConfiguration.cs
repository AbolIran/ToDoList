using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain;

namespace ToDoList.Persistance.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.ApplicationUserId)
                .IsRequired();

            builder.Property(a => a.AddressTitle)
                .HasMaxLength(100);

            builder.Property(a => a.PostalCode)
                .HasMaxLength(20);

            builder.Property(a => a.UserAddress)
                .IsRequired();

            builder.Property(a => a.CountryId)
                .IsRequired();

            builder.Property(a => a.StateId)
                .IsRequired();

            builder.Property(a => a.CityId)
                .IsRequired();

            builder.HasOne(a => a.ApplicationUser)
                .WithMany()
                .HasForeignKey(a => a.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Country)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.State)
                .WithMany(s => s.Addresses)
                .HasForeignKey(a => a.StateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.City)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
