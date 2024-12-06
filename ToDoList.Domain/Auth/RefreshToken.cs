using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Common;

namespace ToDoList.Domain.Auth
{
    public class RefreshToken : BaseDomainEntity
    {
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
