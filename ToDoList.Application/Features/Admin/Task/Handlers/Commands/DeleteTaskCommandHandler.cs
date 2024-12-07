using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Contracts.Persistence;
using ToDoList.Application.Features.Admin.Task.Requests.Commands;
using ToDoList.Application.Helpers;

namespace ToDoList.Application.Features.Admin.Task.Handlers.Commands
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result<EmptyResult>>
    {
        private readonly ILogger<DeleteTaskCommandHandler> _logger;
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskCommandHandler(ILogger<DeleteTaskCommandHandler> logger, ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        public async Task<Result<EmptyResult>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskRepository.Get(request.TaskId.ToString());
                if (task == null)
                {
                    return Result<EmptyResult>.Failure(404, "Task not found");
                }

                await _taskRepository.Delete(task);
                return Result<EmptyResult>.Success(200, "Task deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<EmptyResult>.Failure(500, "An error occurred", ex.Message);
            }
        }
    }

}
