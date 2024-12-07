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
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new[]
            {
                new IdentityUserRole<string>
                {
                    UserId = "69c528e1-dc6d-4319-8f52-89d608cbde7f",
                    RoleId = "03553446-a2b4-4bad-b38b-ebb4c6a86852"
                }
            });
        }
    }
}
