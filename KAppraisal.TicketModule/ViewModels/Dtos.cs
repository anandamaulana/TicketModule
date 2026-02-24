using KAppraisal.TicketModule.Enums;

namespace KAppraisal.TicketModule.ViewModels;

// ─── Pagination ───────────────────────────────────────────────────────────────
public class PaginatedResponse<TDto>
{
    public List<TDto> Data { get; set; } = [];
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}

// ─── Exception ────────────────────────────────────────────────────────────────
public class ExceptionDto
{
    public required int StatusCode { get; set; }
    public string? Message { get; set; }
}

// ─── User ─────────────────────────────────────────────────────────────────────
public class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}

public class UserActionDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public UserRole Role { get; set; }
    public required string TenantId { get; set; }
}

// ─── Ticket ───────────────────────────────────────────────────────────────────
public class TicketDto
{
    public string Id { get; set; } = string.Empty;
    public string TicketNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketCategory Category { get; set; }
    public TicketPriority Priority { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public UserDto? SubmittedBy { get; set; }
    public UserDto? AssignedTo { get; set; }
    public List<TicketCommentDto> Comments { get; set; } = [];
    public List<TicketAttachmentDto> Attachments { get; set; } = [];
}

public class TicketActionDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public TicketCategory Category { get; set; }
    // Priority tidak disertakan — hanya Admin/Staff yang bisa set via PUT /{id}/priority
    public DateTime? Deadline { get; set; }
}

public class AssignTicketDto
{
    public required string StaffId { get; set; }
}

public class UpdateTicketStatusDto
{
    public TicketStatus Status { get; set; }
}

public class UpdateTicketPriorityDto
{
    public TicketPriority Priority { get; set; }
}

// ─── Comment ──────────────────────────────────────────────────────────────────
public class TicketCommentDto
{
    public string Id { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserDto? Author { get; set; }
}

public class TicketCommentActionDto
{
    public required string Content { get; set; }
}

// ─── Attachment ───────────────────────────────────────────────────────────────
public class TicketAttachmentDto
{
    public string Id { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDto? UploadedBy { get; set; }
}
