using ToDoServer.DTOs.Request;
using ToDoServer.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace ToDoServer.Controllers;

[ApiController]
[Route("v1/todo")]
public class ToDoController(IToDoService toDoService) : ControllerBase
{ 
    private readonly IToDoService _toDoService = toDoService;

    /// <summary>
    /// Retrieves a paginated list of to-do tasks.
    /// </summary>
    /// <param name="request">Pagination and filtering parameters.</param>
    /// <returns>A paginated list of to-do tasks.</returns>
    /// <response code="200">Successfully retrieved the list of tasks.</response>
    /// <response code="400">Invalid request parameters.</response>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /v1/todo/get-tasks?pageNumber=1&amp;pageSize=10
    ///
    /// </remarks>
    [HttpGet("get-tasks")]
    [ProducesResponseType(typeof(GetPaginatedTasksRequest), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetTasks([FromQuery] GetPaginatedTasksRequest request)
    {
        var response = await _toDoService.GetPaginatedTasks(request);
        return Ok(response);
    }

    /// <summary>
    /// Creates a new to-do task.
    /// </summary>
    /// <param name="request">The details of the task to create.</param>
    /// <returns>The created task details.</returns>
    /// <response code="201">Task created successfully.</response>
    /// <response code="400">Invalid task details.</response>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /v1/todo/create-task
    ///     {
    ///         "title": "New Task",
    ///         "description": "Task description"
    ///     }
    ///
    /// </remarks>
    [HttpPost("create-task")]
    [ProducesResponseType(typeof(CreateTaskDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto request)
    {
        var response = await _toDoService.CreateTaskAsync(request);
        return CreatedAtAction(nameof(GetTasks), new { id = response.Data.Id }, response);
    }

    /// <summary>
    /// Deletes all completed to-do tasks.
    /// </summary>
    /// <response code="204">All completed tasks deleted successfully.</response>
    /// <response code="400">Failed to delete tasks.</response>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /v1/todo/delete-all-ready
    ///
    /// </remarks>
    [HttpDelete("delete-all-ready")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteAllReady()
    {
        await _toDoService.DeleteAllReadyTasks();
        return NoContent();
    }

    /// <summary>
    /// Updates the status of a to-do task.
    /// </summary>
    /// <param name="request">The details of the task status change.</param>
    /// <response code="204">Task status updated successfully.</response>
    /// <response code="400">Invalid task details.</response>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH /v1/todo/patch-status
    ///     {
    ///         "id": 1,
    ///         "status": true
    ///     }
    ///
    /// </remarks>
    [HttpPatch("patch-status")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> PatchStatus([FromBody] ChangeStatusTaskDto request)
    {
        await _toDoService.ChangeTaskStatus(request);
        return NoContent();
    }

    /// <summary>
    /// Deletes a specific to-do task by ID.
    /// </summary>
    /// <param name="id">The ID of the task to delete.</param>
    /// <response code="204">Task deleted successfully.</response>
    /// <response code="404">Task not found.</response>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /v1/todo/delete-task/1
    ///
    /// </remarks>
    [HttpDelete("delete-task/{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteTask([FromRoute] long id)
    {
        await _toDoService.DeleteTaskById(id);
        return NoContent();
    }

    /// <summary>
    /// Updates the text of a specific to-do task.
    /// </summary>
    /// <param name="request">The details of the text change.</param>
    /// <response code="204">Task text updated successfully.</response>
    /// <response code="400">Invalid task details.</response>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH /v1/todo/patch-text
    ///     {
    ///         "id": 1,
    ///         "text": "Updated task title",
    ///         "description": "Updated task description",
    ///     }
    ///
    /// </remarks>
    [HttpPatch("patch-text")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> PatchText([FromBody] ChangeTextTaskDto request)
    {
        await _toDoService.ChangeTaskTextById(request.Id, request);
        return NoContent();
    }
}