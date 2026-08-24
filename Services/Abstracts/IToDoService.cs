using ToDoServer.DTOs.Response;
using ToDoServer.DTOs.Request;

namespace ToDoServer.Services.Abstracts;

public interface IToDoService
{
    Task<GetPaginatedTasksResponse> GetPaginatedTasks(GetPaginatedTasksRequest request);
    Task<CreateTaskResponse> CreateTaskAsync(CreateTaskDto request);
    Task DeleteAllReadyTasks();
    Task ChangeTaskStatus(ChangeStatusTaskDto request);
    Task DeleteTaskById(long id);
    Task ChangeTaskTextById(long id, ChangeTextTaskDto request);
}
