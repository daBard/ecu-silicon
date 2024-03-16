using Infrastructure.Models;

namespace Infrastructure.Factories;

public class ResponseFactory
{
    public static ResponseResult Ok()
    {
        return new ResponseResult
        {
            Message = "Succeeded",
            StatusCode = StatusCode.OK
        };
    }

    public static ResponseResult Ok(string message = "Succeeded")
    {
        return new ResponseResult
        {
            Message = message,
            StatusCode = StatusCode.OK
        };
    }

    public static ResponseResult Ok(object obj, string message = "Succeeded")
    {
        return new ResponseResult
        {
            ContentResult = obj,
            Message = message,
            StatusCode = StatusCode.OK
        };
    }

    public static ResponseResult Error(string? message)
    {
        return new ResponseResult
        {
            Message = message,
            StatusCode = StatusCode.ERROR
        };
    }

    public static ResponseResult ServerError(string? message)
    {
        return new ResponseResult
        {
            Message = message,
            StatusCode = StatusCode.SERVER_ERROR
        };
    }

    public static ResponseResult NotFound(string? message)
    {
        return new ResponseResult
        {
            Message = message,
            StatusCode = StatusCode.NOT_FOUND
        };
    }

    public static ResponseResult Exists(string? message)
    {
        return new ResponseResult
        {
            Message = message,
            StatusCode = StatusCode.EXISTS
        };
    }
}
