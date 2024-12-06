using Microsoft.AspNetCore.Identity;
using ToDoList.Domain.Auth;
using ToDoList.Domain.Common;

namespace ToDoList.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public Guid UserGuid { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string PhoneNumber { get; set; }
        public override bool PhoneNumberConfirmed { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<Address>? Addresses { get; set; }
        public Gender? Gender { get; set; }
    }
}
