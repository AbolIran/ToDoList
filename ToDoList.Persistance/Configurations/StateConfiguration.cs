using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Common;

namespace ToDoList.Persistance.Configurations
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            // Table name
            builder.ToTable("States");

            // Primary key
            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // Relationships
            builder.HasMany(s => s.Addresses)
                   .WithOne(a => a.State)
                   .HasForeignKey(a => a.StateId)
                   .OnDelete(DeleteBehavior.Restrict);

            // BaseDomainEntity configuration
            builder.Property(s => s.DateCreated)
                   .IsRequired();

            builder.Property(s => s.LastModifiedDate)
                   .IsRequired();
        }
    }
}
