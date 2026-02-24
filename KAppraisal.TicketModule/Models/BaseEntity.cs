using System.ComponentModel.DataAnnotations;
using KAppraisal.TicketModule.Enums;

namespace KAppraisal.TicketModule.Models;

public class BaseEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString().Replace("-", "");

    // OWNERSHIP
    public required string TenantId { get; set; }

    // MONITORING
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Statuses
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    // public List<EntityHistory> Histories { get; set; } = [];
}
