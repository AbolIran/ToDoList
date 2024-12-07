using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.Services.Interfaces
{
    public interface IUserAccessor
    {
        string GetUserName();
        string GetUserId();
    }
}
