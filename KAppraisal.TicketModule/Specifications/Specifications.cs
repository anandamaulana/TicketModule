using Ardalis.Specification;
using KAppraisal.TicketModule.Models;
using KAppraisal.TicketModule.Queries;

namespace KAppraisal.TicketModule.Specifications;

// Sama persis pola dari ServiceModule
public class BaseSpecification<T> : Specification<T>
    where T : BaseEntity
{
    public BaseSpecification(BaseQuery query)
    {
        if (query.Ids != null && query.Ids.Count != 0)
            Query.Where(x => query.Ids.Contains(x.Id));
    }

    protected void Paginate(BaseQuery query)
    {
        if (!query.Paginate) return;
        var skip = (query.Page - 1) * query.PageSize;
        Query.Skip(skip).Take(query.PageSize);
    }
}

public class TicketSpecification : BaseSpecification<Ticket>
{
    public TicketSpecification(TicketQuery query) : base(query)
    {
        Query
            .Include(t => t.SubmittedBy)
            .Include(t => t.AssignedTo);

        if (query.IncludeDetails)
        {
            Query
                .Include(t => t.TicketComments).ThenInclude(c => c.Author)
                .Include(t => t.Attachments).ThenInclude(a => a.UploadedBy);
        }

        if (query.Status.HasValue)
            Query.Where(t => t.TicketStatus == query.Status.Value);

        if (query.Priority.HasValue)
            Query.Where(t => t.Priority == query.Priority.Value);

        if (query.Category.HasValue)
            Query.Where(t => t.Category == query.Category.Value);

        if (!string.IsNullOrEmpty(query.AssignedToId))
            Query.Where(t => t.AssignedToId == query.AssignedToId);

        if (!string.IsNullOrEmpty(query.SubmittedById))
            Query.Where(t => t.SubmittedById == query.SubmittedById);

        if (!string.IsNullOrEmpty(query.Search))
            Query.Where(t =>
                t.Title.ToLower().Contains(query.Search.ToLower()) ||
                t.TicketNumber.ToLower().Contains(query.Search.ToLower())
            );

        Query.Where(t => t.Status != KAppraisal.TicketModule.Enums.EntityStatus.Deleted);
        Query.OrderByDescending(t => t.CreatedAt);
        Paginate(query);
    }
}

public class UserSpecification : BaseSpecification<User>
{
    public UserSpecification(UserQuery query) : base(query)
    {
        if (query.Role.HasValue)
            Query.Where(u => u.Role == query.Role.Value);

        if (!string.IsNullOrEmpty(query.Search))
            Query.Where(u => u.Name.ToLower().Contains(query.Search.ToLower()));

        Query.Where(u => u.Status != KAppraisal.TicketModule.Enums.EntityStatus.Deleted);
        Paginate(query);
    }
}
