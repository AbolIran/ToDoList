using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Application.Contracts.Persistence;
using ToDoList.Application.Features.Admin.Task.Requests.Queries;
using ToDoList.Application.Helpers;

namespace ToDoList.Application.Features.Admin.Task.Handlers.Queries
{
    public class GetTaskDetailRequestHandler : IRequestHandler<GetTaskDetailRequest, Result<Domain.Task>>
    {
        private readonly ILogger<GetTaskDetailRequestHandler> _logger;
        private readonly ITaskRepository _taskRepository;

        public GetTaskDetailRequestHandler(ILogger<GetTaskDetailRequestHandler> logger, ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        public async Task<Result<Domain.Task>> Handle(GetTaskDetailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskRepository.Get(request.TaskId.ToString());
                if (task == null)
                {
                    return Result<Domain.Task>.Failure(404, "Task not found.");
                }

                return Result<Domain.Task>.Success(200, "Task retrieved successfully.", task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<Domain.Task>.Failure(500, "An error occurred while fetching the task.", ex.Message);
            }
        }
    }
}
