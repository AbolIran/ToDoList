using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Helpers;

namespace ToDoList.Application.Features.Admin.Task.Requests.Commands
{
    public class UpdateTaskCommand : IRequest<Result<EmptyResult>>
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}
