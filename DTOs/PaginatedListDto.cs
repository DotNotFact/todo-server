namespace ToDoServer.DTOs;

public class PaginatedListDto<T>
{
    public List<T> Content { get; set; } = [];

    public long NotReady { get; set; }
    public long NumberOfElement { get; set; }
    public long Ready { get; set; }
}
