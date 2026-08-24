using Microsoft.EntityFrameworkCore;
using ToDoServer.Configurations;
using ToDoServer.Entities;

namespace ToDoServer.Data;

public class ToDoContext(DbContextOptions<ToDoContext> options) : DbContext(options)
{
    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
         
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
    }
}