using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.Services.Models
{
    public class ExpiredTokenClaim
    {
        public string UserId { get; set; }
        public string RefreshTokenId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
