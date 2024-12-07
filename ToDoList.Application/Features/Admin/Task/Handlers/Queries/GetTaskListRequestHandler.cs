using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Application.Contracts.Persistence;
using ToDoList.Application.Features.Admin.Task.Requests.Queries;
using ToDoList.Application.Helpers;

namespace ToDoList.Application.Features.Admin.Task.Handlers.Queries
{
    public class GetTaskListRequestHandler : IRequestHandler<GetTaskListRequest, Result<List<Domain.Task>>>
    {
        private readonly ILogger<GetTaskListRequestHandler> _logger;
        private readonly ITaskRepository _taskRepository;

        public GetTaskListRequestHandler(ILogger<GetTaskListRequestHandler> logger, ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        public async Task<Result<List<Domain.Task>>> Handle(GetTaskListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await _taskRepository.GetAll();
                return Result<List<Domain.Task>>.Success(200, "Tasks retrieved successfully.", tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<Domain.Task>>.Failure(500, "An error occurred while fetching the task list.", ex.Message);
            }
        }
    }
}
