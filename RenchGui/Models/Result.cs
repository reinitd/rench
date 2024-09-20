using Newtonsoft.Json;

namespace RenchGui.Models;

public class Result
{
    [JsonProperty("success")]
    public bool Success { get; set; }
    [JsonProperty("message")]
    public string Message { get; set; }

    public Result(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public static Result Empty(bool success, string message)
    {
        return new Result(success, message);
    }

    public static implicit operator Result((bool success, string message) tuple)
    {
        return new Result(tuple.success, tuple.message);
    }
}

public class Result<T> : Result
{
    public T Value { get; set; }

    public Result(bool success, string message, T value) : base(success, message)
    {
        Value = value;
    }

    public new static Result<T> Empty(bool success, string message)
    {
        return new Result<T>(success, message, default);
    }

    public static implicit operator Result<T>((bool success, string message, T value) tuple)
    {
        return new Result<T>(tuple.success, tuple.message, tuple.value);
    }
}
