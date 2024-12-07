using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Common;
using ToDoList.Domain;

namespace ToDoList.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Properties
            builder.Property(au => au.FirstName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(au => au.LastName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(au => au.PhoneNumber)
                   .HasMaxLength(15);

            builder.Property(u => u.Gender)
                   .HasColumnType("int")
                   .IsRequired(false);

            builder.Property(au => au.Email)
                   .IsRequired(false);

            builder.Property(au => au.NormalizedEmail)
                   .IsRequired(false);

            builder.Property(u => u.EmailConfirmed)
                   .HasColumnName("EmailConfirmed")
                   .HasColumnType("bit");

            // Relationships
            builder.HasMany(au => au.Addresses)
                   .WithOne(a => a.ApplicationUser)
                   .HasForeignKey(a => a.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);


            // Seed Admin User with Hashed Password
            var hasher = new PasswordHasher<ApplicationUser>();

            var adminUser = new ApplicationUser
            {
                Id = "69c528e1-dc6d-4319-8f52-89d608cbde7f",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "User",
                Gender = Gender.Male,
                PhoneNumber = "09137135707",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            // Set password hash
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            builder.HasData(adminUser);
        }
    }
}
