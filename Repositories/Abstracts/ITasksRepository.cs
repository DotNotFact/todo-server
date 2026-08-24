using ToDoServer.Entities;

namespace ToDoServer.Repositories.Abstracts;

public interface ITasksRepository
{
    IQueryable<TaskEntity> GetTasksQuery();
    Task<TaskEntity?> GetTaskByIdAsync(long id);
    Task CreateTaskAsync(TaskEntity task);
    Task UpdateTaskAsync(TaskEntity task);
    Task DeleteTaskAsync(long id);
    Task DeleteCompletedTasksAsync();
}
