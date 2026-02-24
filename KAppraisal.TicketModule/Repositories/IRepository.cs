using KAppraisal.TicketModule.Models;

namespace KAppraisal.TicketModule.Repositories;

public interface IRepository<T> : IRootRepository<T>
    where T : BaseEntity
{
    Task SoftDeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task ArchiveAsync(T entity, CancellationToken cancellationToken = default);
}
