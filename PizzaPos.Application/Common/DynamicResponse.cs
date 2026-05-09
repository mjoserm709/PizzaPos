namespace PizzaPos.Application.Common;

public class DynamicResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public static DynamicResponse<T> CreateSuccess(T data, string message = "Operación exitosa")
    {
        return new DynamicResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static DynamicResponse<T> CreateError(string message, List<string>? errors = null)
    {
        return new DynamicResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }
}
