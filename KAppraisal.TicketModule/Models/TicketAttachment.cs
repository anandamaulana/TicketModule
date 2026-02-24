namespace KAppraisal.TicketModule.Models;

public class TicketAttachment : BaseEntity
{
    public string TicketId { get; set; } = string.Empty;
    public Ticket? Ticket { get; set; }

    public string UploadedById { get; set; } = string.Empty;
    public User? UploadedBy { get; set; }

    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long FileSize { get; set; }
}
