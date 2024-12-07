using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Dtos.Auth.Interfaces;

namespace ToDoList.Application.Dtos.Auth
{
    public class RefreshTokenDto : IRefreshTokenDto
    {
        public string RefreshToken { get; set; }
    }
}
