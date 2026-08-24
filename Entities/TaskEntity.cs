namespace ToDoServer.Entities;

public class TaskEntity
{
    public long Id { get; set; }

    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public bool Status { get; set; }
}
