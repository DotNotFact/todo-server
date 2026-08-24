namespace ToDoServer.Templates;

public class BaseSuccessResponse<T>
{
    public int StatusCode { get; set; }
    public bool Success { get; set; }

    public string? Message { get; set; }
}
