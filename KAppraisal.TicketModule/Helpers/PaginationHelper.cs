using KAppraisal.TicketModule.Queries;
using KAppraisal.TicketModule.ViewModels;

namespace KAppraisal.TicketModule.Helpers;

public static class PaginationHelper
{
    public static PaginatedResponse<TDto> Page<TDto>(
        BaseQuery query,
        List<TDto> data,
        int totalItems
    )
    {
        var totalPages = query.PageSize > 0
            ? (int)Math.Ceiling((double)totalItems / query.PageSize)
            : 1;

        return new PaginatedResponse<TDto>
        {
            Data = data,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalItems = totalItems,
            TotalPages = totalPages,
        };
    }
}
