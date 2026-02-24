using KAppraisal.TicketModule.Models;
using KAppraisal.TicketModule.ViewModels;
using Mapster;

namespace KAppraisal.TicketModule.Mappers;

public static class MappingConfig
{
    public static void RegisterMappings(this TypeAdapterConfig config)
    {
        config
            .NewConfig<TicketActionDto, Ticket>()
            .Map(dest => dest.Id, src => Guid.NewGuid().ToString("N"))
            .Ignore(dest => dest.TicketNumber!)
            .Ignore(dest => dest.SubmittedById)
            .Ignore(dest => dest.SubmittedBy!)
            .Ignore(dest => dest.AssignedToId!)
            .Ignore(dest => dest.AssignedTo!)
            .Ignore(dest => dest.Priority)  // Priority diset manual di service, bukan dari DTO
            .Ignore(dest => dest.TenantId)
            // .Ignore(dest => dest.Histories)
            .IgnoreNullValues(true);

        config
            .NewConfig<Ticket, TicketDto>()
            .Map(dest => dest.SubmittedBy, src => src.SubmittedBy)
            .Map(dest => dest.AssignedTo, src => src.AssignedTo)
            .Map(dest => dest.Comments, src => src.TicketComments)
            .Map(dest => dest.Attachments, src => src.Attachments)
            .IgnoreNullValues(true);

        config
            .NewConfig<TicketComment, TicketCommentDto>()
            .Map(dest => dest.Author, src => src.Author);

        config
            .NewConfig<TicketAttachment, TicketAttachmentDto>()
            .Map(dest => dest.UploadedBy, src => src.UploadedBy);

        config
            .NewConfig<UserActionDto, User>()
            .Map(dest => dest.Id, src => Guid.NewGuid().ToString("N"))
            // .Ignore(dest => dest.Histories)
            .IgnoreNullValues(true);

        config.NewConfig<User, UserDto>();
    }
}
