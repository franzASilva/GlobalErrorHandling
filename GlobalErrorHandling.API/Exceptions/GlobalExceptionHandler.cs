using GlobalErrorHandling.Domain.Exceptions;
using GlobalErrorHandling.Domain.Exceptions.Constants;
using GlobalErrorHandling.Domain.Model;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace GlobalErrorHandling.API.Exceptions;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var statusError = GetStatusCode(exception);

        var problemDetails = new
        {
            Status = (int)statusError,
            Title = statusError.ToString(),
            Detail = new {
                Instance = httpContext.Request.Path,
                Errors = GetErrors(exception)
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static ErrorDetailModel[] GetErrors(Exception exception) => exception switch
    {
        Exception ex when ex is BusinessValidationException businessValidationException => businessValidationException.Errors,
        Exception ex when ex is NotFoundException notFoundException => notFoundException.Errors,
        Exception ex when ex is ProductException productException => productException.Errors,
        _ => [new ErrorDetailModel(exception!.Message, ExceptionType.InternalError, exception!.InnerException?.Message)]
    };

    private static HttpStatusCode GetStatusCode(Exception exception) => exception switch
    {
        Exception ex when ex is NotFoundException => HttpStatusCode.NotFound,
        Exception ex when ex is BusinessValidationException => HttpStatusCode.BadRequest,
        Exception ex when ex is ProductException => HttpStatusCode.UnavailableForLegalReasons,
        _ => HttpStatusCode.InternalServerError
    };
}
