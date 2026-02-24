using KAppraisal.TicketModule.Contexts;
using KAppraisal.TicketModule.Enums;
using KAppraisal.TicketModule.Exceptions;
using KAppraisal.TicketModule.Helpers;
using KAppraisal.TicketModule.Models;
using KAppraisal.TicketModule.Queries;
using KAppraisal.TicketModule.Repositories;
using KAppraisal.TicketModule.Specifications;
using KAppraisal.TicketModule.ViewModels;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace KAppraisal.TicketModule.Services;

public class TicketService(
    IRepository<Ticket> ticketRepository,
    IRepository<TicketComment> commentRepository,
    IRepository<TicketAttachment> attachmentRepository,
    TicketDbContext dbContext,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : ITicketService
{
    private string GetUserId()
    {
        var userId = httpContextAccessor.HttpContext?.Request.Headers["X-USER-ID"].ToString();
        AppException.ThrowIfTrue(string.IsNullOrEmpty(userId), ExceptionType.Unauthorized, "X-USER-ID header is required.");
        return userId!;
    }

    private async Task<string> GenerateTicketNumber()
    {
        var year = DateTime.UtcNow.Year;
        var count = await dbContext.Tickets.CountAsync(t => t.CreatedAt.Year == year);
        return $"TKT-{year}-{(count + 1):D3}";
    }

    public async Task<PaginatedResponse<TicketDto>> GetAllAsync(TicketQuery query)
    {
        var spec = new TicketSpecification(query);
        var items = await ticketRepository.ListAsync(spec);
        var total = await ticketRepository.CountAsync(spec);
        var dtos = mapper.Map<List<TicketDto>>(items);
        return PaginationHelper.Page(query, dtos, total);
    }

    public async Task<PaginatedResponse<TicketDto>> GetMyTicketsAsync(TicketQuery query)
    {
        var userId = GetUserId();
        query.SubmittedById = userId; // force filter hanya tiket milik user yang login
        var spec = new TicketSpecification(query);
        var items = await ticketRepository.ListAsync(spec);
        var total = await ticketRepository.CountAsync(spec);
        var dtos = mapper.Map<List<TicketDto>>(items);
        return PaginationHelper.Page(query, dtos, total);
    }

    public async Task<TicketDto> GetByIdAsync(string id)
    {
        var query = new TicketQuery { Ids = [id], IncludeDetails = true };
        var spec = new TicketSpecification(query);
        var ticket = await ticketRepository.FirstOrDefaultAsync(spec);
        AppException.ThrowIfNull(ticket, ExceptionType.NotFound, $"Ticket with ID={id} not found.");
        return mapper.Map<TicketDto>(ticket);
    }

    public async Task<TicketDto> CreateAsync(TicketActionDto dto)
    {
        var userId = GetUserId();
        var ticket = mapper.Map<Ticket>(dto);
        ticket.TicketNumber = await GenerateTicketNumber();
        ticket.SubmittedById = userId;
        ticket.TicketStatus = TicketStatus.Open;
        ticket.Priority = TicketPriority.Low; // default Low — Admin/Staff yang akan set priority via endpoint
        ticket.TenantId = "default";
        var result = await ticketRepository.AddAsync(ticket);
        return mapper.Map<TicketDto>(result);
    }

    public async Task UpdateAsync(string id, TicketActionDto dto)
    {
        var ticket = await ticketRepository.GetByIdAsync(id);
        AppException.ThrowIfNull(ticket, ExceptionType.NotFound, $"Ticket with ID={id} not found.");
        mapper.Map(dto, ticket);
        ticket.UpdatedAt = DateTime.UtcNow;
        await ticketRepository.UpdateAsync(ticket);
    }

    public async Task AssignAsync(string id, AssignTicketDto dto)
    {
        var ticket = await ticketRepository.GetByIdAsync(id);
        AppException.ThrowIfNull(ticket, ExceptionType.NotFound, $"Ticket with ID={id} not found.");
        ticket.AssignedToId = dto.StaffId;
        ticket.TicketStatus = TicketStatus.InProgress;
        ticket.UpdatedAt = DateTime.UtcNow;
        await ticketRepository.UpdateAsync(ticket);
    }

    public async Task UpdateStatusAsync(string id, UpdateTicketStatusDto dto)
    {
        var ticket = await ticketRepository.GetByIdAsync(id);
        AppException.ThrowIfNull(ticket, ExceptionType.NotFound, $"Ticket with ID={id} not found.");
        ticket.TicketStatus = dto.Status;
        ticket.UpdatedAt = DateTime.UtcNow;
        await ticketRepository.UpdateAsync(ticket);
    }

    public async Task UpdatePriorityAsync(string id, UpdateTicketPriorityDto dto)
    {
        var ticket = await ticketRepository.GetByIdAsync(id);
        AppException.ThrowIfNull(ticket, ExceptionType.NotFound, $"Ticket with ID={id} not found.");
        ticket.Priority = dto.Priority;
        ticket.UpdatedAt = DateTime.UtcNow;
        await ticketRepository.UpdateAsync(ticket);
    }

    public async Task DeleteAsync(string id)
    {
        var ticket = await ticketRepository.GetByIdAsync(id);
        AppException.ThrowIfNull(ticket, ExceptionType.NotFound, $"Ticket with ID={id} not found.");
        await ticketRepository.SoftDeleteAsync(ticket);
    }

    public async Task<TicketCommentDto> AddCommentAsync(string ticketId, TicketCommentActionDto dto)
    {
        var userId = GetUserId();
        var comment = new TicketComment
        {
            Id = Guid.NewGuid().ToString("N"),
            TicketId = ticketId,
            AuthorId = userId,
            Content = dto.Content,
            TenantId = "default",
        };
        var result = await commentRepository.AddAsync(comment);
        return mapper.Map<TicketCommentDto>(result);
    }

    public async Task DeleteCommentAsync(string ticketId, string commentId)
    {
        var comment = await commentRepository.GetByIdAsync(commentId);
        AppException.ThrowIfNull(comment, ExceptionType.NotFound, $"Comment with ID={commentId} not found.");
        AppException.ThrowIfTrue(comment.TicketId != ticketId, ExceptionType.BadRequest, "Comment does not belong to this ticket.");
        await commentRepository.SoftDeleteAsync(comment);
    }

public async Task<TicketAttachmentDto> AddAttachmentAsync(string ticketId, IFormFile file)
    {
        var userId = GetUserId();
        var folder = Path.Combine("wwwroot", "uploads", ticketId);
        Directory.CreateDirectory(folder);

        var uniqueFileName = $"{Guid.NewGuid():N}_{file.FileName}";
        var filePath = Path.Combine(folder, uniqueFileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var attachment = new TicketAttachment
        {
            Id = Guid.NewGuid().ToString("N"),
            TicketId = ticketId,
            UploadedById = userId,
            FileName = file.FileName,
            FilePath = $"/uploads/{ticketId}/{uniqueFileName}",
            FileSize = file.Length,
            TenantId = "default",
        };
        var result = await attachmentRepository.AddAsync(attachment);
        return mapper.Map<TicketAttachmentDto>(result);
    }

    public async Task DeleteAttachmentAsync(string ticketId, string attachmentId)
    {
        var attachment = await attachmentRepository.GetByIdAsync(attachmentId);
        AppException.ThrowIfNull(attachment, ExceptionType.NotFound, $"Attachment with ID={attachmentId} not found.");
        AppException.ThrowIfTrue(attachment.TicketId != ticketId, ExceptionType.BadRequest, "Attachment does not belong to this ticket.");

        if (File.Exists(attachment.FilePath))
            File.Delete(attachment.FilePath);

        await attachmentRepository.SoftDeleteAsync(attachment);
    }

}
