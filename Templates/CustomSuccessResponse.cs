namespace ToDoServer.Templates;

public class CustomSuccessResponse<T> : BaseSuccessResponse<T>
{
    public required T Data { get; set; }
}
