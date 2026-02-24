using KAppraisal.TicketModule.Enums;
using KAppraisal.TicketModule.Models;
using Microsoft.EntityFrameworkCore;

namespace KAppraisal.TicketModule.Repositories;

public class Repository<T>(DbContext context) : RootRepository<T>(context), IRepository<T>
    where T : BaseEntity
{
    public async Task ArchiveAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.Status = EntityStatus.Archived;
        await UpdateAsync(entity, cancellationToken);
    }

    public async Task SoftDeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.Status = EntityStatus.Deleted;
        await UpdateAsync(entity, cancellationToken);
    }
}
