using System.Diagnostics.CodeAnalysis;
using KAppraisal.TicketModule.Enums;

namespace KAppraisal.TicketModule.Exceptions;

public class AppException(ExceptionType type, string? message = null) : Exception
{
    public ExceptionType Type { get; } = type;

    public string? ExceptionMessage { get; } = message;

    public static void ThrowIfNull(
        [NotNull] object? obj,
        ExceptionType type = ExceptionType.NotFound,
        string? message = null
    )
    {
        if (obj == null)
            throw new AppException(type, message);
    }

    public static void ThrowIfTrue(
        bool condition,
        ExceptionType type = ExceptionType.BadRequest,
        string? message = null
    )
    {
        if (condition)
            throw new AppException(type, message);
    }

    public static void ThrowIfFalse(
        bool condition,
        ExceptionType type = ExceptionType.BadRequest,
        string? message = null
    )
    {
        if (!condition)
            throw new AppException(type, message);
    }

    public string BuildMessage(string? message = null)
    {
        message ??= Type switch
        {
            ExceptionType.BadRequest
                => "Bad Request: The request could not be understood or was missing required parameters.",
            ExceptionType.Unauthorized
                => "Unauthorized: Authentication is required and has failed or has not yet been provided.",
            ExceptionType.Forbidden
                => "Forbidden: You do not have permission to access the requested resource.",
            ExceptionType.NotFound => "Not Found: The requested resource could not be found.",
            ExceptionType.Conflict
                => "Conflict: The request could not be completed due to a conflict with the current state of the resource.",
            ExceptionType.ServiceUnavailable
                => "Service Not Available: Please wait for a moment or contact customer support",
            _
                => "Internal Server Error: An unexpected error occurred on the server. Please try again later.",
        };

        return message;
    }
}
