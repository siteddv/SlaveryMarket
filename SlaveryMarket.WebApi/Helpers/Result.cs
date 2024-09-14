using System.Net;

namespace SlaveryMarket.Helpers;

public class Result<T>
{
    public bool Succeeded { get; set; }
    public IEnumerable<string>? Errors { get; set; }
    public T? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    
    public Result(bool succeeded, IEnumerable<string>? errors, T? data, HttpStatusCode statusCode)
    {
        Succeeded = succeeded;
        Errors = errors;
        Data = data;
        StatusCode = statusCode;
    }
    
    public static Result<T> Success() => new(true, null, default, HttpStatusCode.OK);
    
    
    public static Result<T> Success(T data) => new(true, null, data, HttpStatusCode.OK);
    
    public static Result<T> BadRequest(IEnumerable<string> errors) =>
        new(false, errors, default, HttpStatusCode.BadRequest);
    
    public static Result<T> BadRequest(string error) =>
        new(false, new List<string> { error }, default, HttpStatusCode.BadRequest);
    
    public static Result<T> InternalServerError(string error)
        => new(false, new List<string> { error }, default, HttpStatusCode.InternalServerError);
    
    public static Result<T> NotFound(string error) =>
        new(false, new List<string> { error }, default, HttpStatusCode.NotFound);
}