using KAppraisal.TicketModule.Mappers;
using Mapster;
using MapsterMapper;

namespace KAppraisal.TicketModule.Extensions;

public static class MapperExtension
{
    public static void AddMapperConfiguration(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.RegisterMappings();
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}
