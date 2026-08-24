using System.ComponentModel.DataAnnotations;

namespace ToDoServer.DTOs.Request;

public class ChangeStatusTaskDto
{
    [Required]
    public long Id { get; set; }
    [Required]
    public bool Status { get; set; }
}
