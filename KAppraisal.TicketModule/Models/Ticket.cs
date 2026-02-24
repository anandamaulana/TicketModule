using KAppraisal.TicketModule.Enums;

namespace KAppraisal.TicketModule.Models;

public class Ticket : BaseEntity
{
    public string TicketNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketCategory Category { get; set; }
    public TicketPriority Priority { get; set; }
    public TicketStatus TicketStatus { get; set; } = TicketStatus.Open;
    public DateTime? Deadline { get; set; }

    // siapa yang submit
    public string SubmittedById { get; set; } = string.Empty;
    public User? SubmittedBy { get; set; }

    // staff yang di-assign
    public string? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }

    public List<TicketComment> TicketComments { get; set; } = [];
    public List<TicketAttachment> Attachments { get; set; } = [];
}
