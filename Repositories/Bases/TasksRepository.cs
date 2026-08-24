using ToDoServer.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using ToDoServer.Entities;
using ToDoServer.Data;

namespace ToDoServer.Repositories.Bases;

public class TasksRepository(ToDoContext context) : ITasksRepository
{
    private readonly ToDoContext _context = context;

    public IQueryable<TaskEntity> GetTasksQuery()
    {
        var entities = _context.Tasks.AsQueryable();
        return entities;
    }

    public async Task<TaskEntity?> GetTaskByIdAsync(long id)
    {
        var entity = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        return entity;
    }

    public async Task CreateTaskAsync(TaskEntity task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(TaskEntity task)
    {
        await _context.Tasks
            .Where(t => t.Id == task.Id)
            .ExecuteUpdateAsync(sp => sp
                .SetProperty(t => t.Description, task.Description)
                .SetProperty(t => t.UpdatedAt, task.UpdatedAt)
                .SetProperty(t => t.Status, task.Status)
                .SetProperty(t => t.Title, task.Title)
                );
    }

    public async Task DeleteTaskAsync(long id)
    {
        await _context.Tasks
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task DeleteCompletedTasksAsync()
    {
        var completedTasks = await _context.Tasks.Where(t => t.Status).ToListAsync();

        _context.Tasks.RemoveRange(completedTasks);
        await _context.SaveChangesAsync();
    }
}
