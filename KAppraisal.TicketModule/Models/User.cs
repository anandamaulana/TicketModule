using KAppraisal.TicketModule.Enums;

namespace KAppraisal.TicketModule.Models;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }

    public List<Ticket> SubmittedTickets { get; set; } = [];
    public List<Ticket> AssignedTickets { get; set; } = [];
    public List<TicketComment> Comments { get; set; } = [];
}
