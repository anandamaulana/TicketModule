using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KAppraisal.TicketModule.Repositories;

public class RootRepository<T>(DbContext context) : RepositoryBase<T>(context), IRootRepository<T>
    where T : class { }
