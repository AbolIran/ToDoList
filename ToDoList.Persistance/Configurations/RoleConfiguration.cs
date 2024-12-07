using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Persistance.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole
            {
                Id = "03553446-a2b4-4bad-b38b-ebb4c6a86852",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });

            builder.HasData(new IdentityRole
            {
                Id = "609effb8-a2d2-49b9-8347-fd74afb2b8f4",
                Name = "User",
                NormalizedName = "ApplicationUser"
            });

        }
    }
}
