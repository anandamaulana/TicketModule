using KAppraisal.TicketModule.Queries;
using KAppraisal.TicketModule.Services;
using KAppraisal.TicketModule.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KAppraisal.TicketModule.Controllers;

public class TicketsController(ITicketService service) : RootController
{
    // ─── Admin/Staff: semua tiket ──────────────────────────────────────────────
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<TicketDto>>> GetAll([FromQuery] TicketQuery query)
        => Ok(await service.GetAllAsync(query));

    // ─── User: hanya tiket milik sendiri ──────────────────────────────────────
    [HttpGet("my")]
    public async Task<ActionResult<PaginatedResponse<TicketDto>>> GetMyTickets([FromQuery] TicketQuery query)
        => Ok(await service.GetMyTicketsAsync(query));

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetById(string id)
        => Ok(await service.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<TicketDto>> Create(TicketActionDto dto)
        => Ok(await service.CreateAsync(dto));

    [HttpPut("{id}")]
    public Task<IActionResult> Update(string id, TicketActionDto dto)
        => Run(() => service.UpdateAsync(id, dto));

    [HttpPut("{id}/assign")]
    public Task<IActionResult> Assign(string id, AssignTicketDto dto)
        => Run(() => service.AssignAsync(id, dto));

    [HttpPut("{id}/status")]
    public Task<IActionResult> UpdateStatus(string id, UpdateTicketStatusDto dto)
        => Run(() => service.UpdateStatusAsync(id, dto));

    [HttpPut("{id}/priority")]
    public Task<IActionResult> UpdatePriority(string id, UpdateTicketPriorityDto dto)
        => Run(() => service.UpdatePriorityAsync(id, dto));

    [HttpDelete("{id}")]
    public Task<IActionResult> Delete(string id)
        => Run(() => service.DeleteAsync(id));

    [HttpPost("{id}/comments")]
    public async Task<ActionResult<TicketCommentDto>> AddComment(string id, TicketCommentActionDto dto)
        => Ok(await service.AddCommentAsync(id, dto));

    [HttpDelete("{id}/comments/{commentId}")]
    public Task<IActionResult> DeleteComment(string id, string commentId)
        => Run(() => service.DeleteCommentAsync(id, commentId));

    [HttpPost("{id}/attachments")]
    public async Task<ActionResult<TicketAttachmentDto>> AddAttachment(string id, IFormFile file)
        => Ok(await service.AddAttachmentAsync(id, file));

    [HttpDelete("{id}/attachments/{attachmentId}")]
    public Task<IActionResult> DeleteAttachment(string id, string attachmentId)
        => Run(() => service.DeleteAttachmentAsync(id, attachmentId));
}
