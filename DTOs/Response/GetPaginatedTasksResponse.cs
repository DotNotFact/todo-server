using ToDoServer.Templates;
using ToDoServer.Entities;
using ToDoServer.DTOs;

namespace ToDoServer.DTOs.Response;

public class GetPaginatedTasksResponse : CustomSuccessResponse<PaginatedListDto<TaskEntity>>
{
}
