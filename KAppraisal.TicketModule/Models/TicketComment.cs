namespace KAppraisal.TicketModule.Models;

public class TicketComment : BaseEntity
{
    public string TicketId { get; set; } = string.Empty;
    public Ticket? Ticket { get; set; }

    public string AuthorId { get; set; } = string.Empty;
    public User? Author { get; set; }

    public string Content { get; set; } = string.Empty;
}
