using Ardalis.Specification;

namespace KAppraisal.TicketModule.Repositories;

public interface IRootRepository<T> : IRepositoryBase<T>
    where T : class { }
