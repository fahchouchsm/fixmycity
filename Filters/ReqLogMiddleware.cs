using Microsoft.AspNetCore.Http;
using System.Diagnostics;

public class ReqLogMiddleware
{
    private readonly RequestDelegate _next;

    public ReqLogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();

        var status = context.Response.StatusCode;

        string color = status switch
        {
            >= 200 and < 300 => "\u001b[32m",
            >= 300 and < 400 => "\u001b[33m",
            _ => "\u001b[31m"
        };
        // string reset = "\u001b[0m";

        // Console.WriteLine(
        //     $"{context.Request.Method} " +
        //     $"{context.Request.Path} -> " +
        //     $"{color}{status}{reset} ({stopwatch.ElapsedMilliseconds}ms)"
        // );
    }
}