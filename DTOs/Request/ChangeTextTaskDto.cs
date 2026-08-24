using System.ComponentModel.DataAnnotations;

namespace ToDoServer.DTOs.Request;

public class ChangeTextTaskDto
{
    [Required]
    public long Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
}
