using ToDoServer.DTOs.Response;
using ToDoServer.Repositories.Abstracts;
using ToDoServer.DTOs.Request;
using Microsoft.EntityFrameworkCore;
using ToDoServer.Services.Abstracts;
using ToDoServer.DTOs;
using ToDoServer.Entities;

namespace ToDoServer.Services.Bases;

public class ToDoService(ITasksRepository tasksRepository) : IToDoService
{
    private readonly ITasksRepository _tasksRepository = tasksRepository;

    public async Task<GetPaginatedTasksResponse> GetPaginatedTasks(GetPaginatedTasksRequest request)
    {
        var query = _tasksRepository.GetTasksQuery();

        if (request.Status.HasValue)
            query = query.Where(t => t.Status == request.Status.Value);

        var totalCount = await query.CountAsync();

        var tasks = await query
            .Skip((request.Page - 1) * request.PerPage)
            .Take(request.PerPage)
            .ToListAsync();

        var response = new GetPaginatedTasksResponse
        {
            Data = new PaginatedListDto<TaskEntity>
            {
                Content = tasks,
                NotReady = tasks.Count(t => !t.Status),
                Ready = tasks.Count(t => t.Status),
                NumberOfElement = totalCount
            },
            StatusCode = 200,
            Success = true
        };

        return response;
    }

    public async Task<CreateTaskResponse> CreateTaskAsync(CreateTaskDto request)
    {
        var newTask = new TaskEntity
        {
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Status = false
        };

        await _tasksRepository.CreateTaskAsync(newTask);

        var response = new CreateTaskResponse
        {
            Data = newTask,
            StatusCode = 201,
            Success = true
        };

        return response;
    }

    public async Task DeleteTaskById(long id)
    {
        await _tasksRepository.DeleteTaskAsync(id);
    }

    public async Task DeleteAllReadyTasks()
    {
        await _tasksRepository.DeleteCompletedTasksAsync();
    }

    public async Task ChangeTaskStatus(ChangeStatusTaskDto request)
    {
        var task = await _tasksRepository.GetTaskByIdAsync(request.Id);

        if (task is not null)
        {
            task.Status = request.Status;
            task.UpdatedAt = DateTime.UtcNow;
            await _tasksRepository.UpdateTaskAsync(task);
        }
    }

    public async Task ChangeTaskTextById(long id, ChangeTextTaskDto request)
    {
        var task = await _tasksRepository.GetTaskByIdAsync(id);

        if (task is not null)
        {
            task.Title = request.Title;
            task.Description = request.Description;
            task.UpdatedAt = DateTime.UtcNow;

            await _tasksRepository.UpdateTaskAsync(task);
        }
    }
}
