using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Helpers;

namespace ToDoList.Application.Features.Admin.Task.Requests.Queries
{
    public class GetTaskListRequest : IRequest<Result<List<Domain.Task>>>
    {
    }
}
