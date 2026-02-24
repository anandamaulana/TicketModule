namespace KAppraisal.TicketModule.Enums;

public enum TicketStatus
{
    Open = 1,
    InProgress = 2,
    Resolved = 3,
    Closed = 4,
}

public enum TicketCategory
{
    Bug = 1,
    FeatureRequest = 2,
    Improvement = 3,
    Question = 4,
    Other = 5,
}

public enum TicketPriority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4,
}

public enum UserRole
{
    User = 1,
    Admin = 0,
}
