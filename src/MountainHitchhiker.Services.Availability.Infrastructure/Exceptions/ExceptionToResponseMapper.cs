using System.Net;
using Convey.WebApi.Exceptions;
using MountainHitchhiker.Services.Availability.Application.Exceptions;
using MountainHitchhiker.Services.Availability.Core.Exceptions;

namespace MountainHitchhiker.Services.Availability.Infrastructure.Exceptions;

internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            DomainException ex => new ExceptionResponse(new { code = ex.Code, reason = ex.Message },
                HttpStatusCode.BadRequest),
            AppException ex => new ExceptionResponse(new { code = ex.Code, reason = ex.Message },
                HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(new { code = "error", reason = "there was an error." },
                HttpStatusCode.InternalServerError)
        };
}