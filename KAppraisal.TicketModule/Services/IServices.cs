using KAppraisal.TicketModule.ViewModels;
using KAppraisal.TicketModule.Queries;

namespace KAppraisal.TicketModule.Services;

public interface ITicketService
{
    Task<PaginatedResponse<TicketDto>> GetAllAsync(TicketQuery query);
    Task<PaginatedResponse<TicketDto>> GetMyTicketsAsync(TicketQuery query);
    Task<TicketDto> GetByIdAsync(string id);
    Task<TicketDto> CreateAsync(TicketActionDto dto);
    Task UpdateAsync(string id, TicketActionDto dto);
    Task AssignAsync(string id, AssignTicketDto dto);
    Task UpdateStatusAsync(string id, UpdateTicketStatusDto dto);
    Task UpdatePriorityAsync(string id, UpdateTicketPriorityDto dto);
    Task DeleteAsync(string id);
    Task<TicketCommentDto> AddCommentAsync(string ticketId, TicketCommentActionDto dto);
    Task DeleteCommentAsync(string ticketId, string commentId);
    Task<TicketAttachmentDto> AddAttachmentAsync(string ticketId, IFormFile file);
    Task DeleteAttachmentAsync(string ticketId, string attachmentId);
}

public interface IUserService
{
    Task<PaginatedResponse<UserDto>> GetAllAsync(UserQuery query);
    Task<UserDto> GetByIdAsync(string id);
    Task<UserDto> CreateAsync(UserActionDto dto);
    Task<UserDto> GetMeAsync();
}
