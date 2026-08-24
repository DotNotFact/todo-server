using System.ComponentModel.DataAnnotations;

namespace ToDoServer.DTOs.Request;

public class GetPaginatedTasksRequest
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
    public int Page { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "PerPage must be between 1 and 100")]
    public int PerPage { get; set; }
    public bool? Status { get; set; } // Необязательный параметр
}
