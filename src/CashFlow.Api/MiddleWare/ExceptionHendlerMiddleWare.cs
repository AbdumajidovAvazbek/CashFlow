using CashFlow.Api.Helpers;
using CashFlow.Service.Exceptions;

namespace CashFlow.Api.MiddleWare;

public class ExceptionHendlerMiddleWare
{
    private readonly RequestDelegate _next;

    public ExceptionHendlerMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CashFlowException ex)
        {
            context.Response.StatusCode = ex._statusCode;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Code = ex._statusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Code = 500,
                Message = ex.Message
            });
        }
    }
}
