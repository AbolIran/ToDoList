using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dtos.Admin.Task;
using ToDoList.Application.Features.Admin.Task.Handlers.Queries;
using ToDoList.Application.Features.Admin.Task.Requests.Commands;
using ToDoList.Application.Features.Admin.Task.Requests.Queries;

namespace ToDoList.API.Controllers.Admin.Task
{
    [RequireHttps]
    [Route("[controller]")]
    [ApiController]
    public class TaskController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto createTaskDto)
        {
            return HandleResult(await Mediator.Send(new CreateTaskCommand { CreateTaskDto = createTaskDto }));
        }
        [HttpGet("{TaskId}")]
        public async Task<IActionResult> Get(string TaskId)
        {
            return HandleResult(await Mediator.Send(new GetTaskDetailRequest { TaskId = taskId }));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await Mediator.Send(new GetTasksListRequest()));
        }
        [HttpPut("{TaskId}")]
        public async Task<IActionResult> Put(string taskId, [FromBody] UpdateTaskDto updateTaskDto)
        {
            return HandleResult(await Mediator.Send(new UpdateTaskCommand { TaskId = taskId, UpdateTaskDto = updateTaskDto }));
        }
        [HttpDelete("{TaskId}")]
        public async Task<IActionResult> Delete(string taskId)
        {
            return HandleResult(await Mediator.Send(new DeleteTaskCommand { TaskId = taskId }));
        }
    }   
}
