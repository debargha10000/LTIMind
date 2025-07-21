using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace QuickBite.API.src.utils;

public class ResponseEntity<T>
{
    public required HttpStatusCode Code { get; set; }
    public required string Message { get; set; }
    public bool Status { get; set; } = true;
    public T? Data { get; set; } = default;

    [SetsRequiredMembers]
    public ResponseEntity(HttpStatusCode code, string message)
    {
        Code = code;
        Message = message;
    }

    [SetsRequiredMembers]
    public ResponseEntity(HttpStatusCode code, string message, T data)
    {
        Code = code;
        Message = message;
        Data = data;
    }

    [SetsRequiredMembers]
    public ResponseEntity(HttpStatusCode code, string message, bool status)
    {
        Code = code;
        Status = status;
        Message = message;
        Data = default;
    }

    [SetsRequiredMembers]
    public ResponseEntity(HttpStatusCode code, string message, bool status, T data)
    {
        Code = code;
        Status = status;
        Message = message;
        Data = data;
    }

    public IActionResult Build()
    {
        return new ObjectResult(this)
        {
            StatusCode = (int)Code
        };
    }

}
