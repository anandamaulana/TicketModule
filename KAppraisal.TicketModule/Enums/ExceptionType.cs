namespace KAppraisal.TicketModule.Enums;

public enum ExceptionType
{
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409,
    Internal = 500,
    ServiceUnavailable = 502,
}
