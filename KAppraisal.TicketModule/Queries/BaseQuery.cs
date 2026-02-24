using KAppraisal.TicketModule.Enums;

namespace KAppraisal.TicketModule.Queries;

// Sama seperti ServiceModule + tambahan pagination & search
public class BaseQuery
{
    public List<string>? Ids { get; set; }
    public string? Search { get; set; }
    public bool Paginate { get; set; } = false;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class TicketQuery : BaseQuery
{
    public TicketStatus? Status { get; set; }
    public TicketPriority? Priority { get; set; }
    public TicketCategory? Category { get; set; }
    public string? AssignedToId { get; set; }
    public string? SubmittedById { get; set; }
    public bool IncludeDetails { get; set; } = false;
}

public class UserQuery : BaseQuery
{
    public UserRole? Role { get; set; }
}
