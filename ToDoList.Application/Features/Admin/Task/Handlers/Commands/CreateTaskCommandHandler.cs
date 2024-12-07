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
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<EmptyResult>>
    {
        private readonly ILogger<CreateTaskCommandHandler> _logger;
        private readonly ITaskRepository _taskRepository;

        public CreateTaskCommandHandler(ILogger<CreateTaskCommandHandler> logger,
            ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }
        public async Task<Result<EmptyResult>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var task = new Domain.Task
                {
                    Title = request.Title,
                    Description = request.Description,
                    IsCompleted = false,
                    DueDate = request.DueDate
                };

                await _taskRepository.Add(task);

                return Result<EmptyResult>.Success(200, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<EmptyResult>.Failure(500, "", ex.Message);
            }
        }
    }
}
