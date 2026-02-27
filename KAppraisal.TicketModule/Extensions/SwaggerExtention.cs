// using Microsoft.OpenApi.Models;
// using Swashbuckle.AspNetCore.SwaggerGen;

// namespace KAppraisal.TicketModule.Extensions;

// public static class SwaggerExtension
// {
//     public static IServiceCollection AddSwaggerWithUserIdHeader(this IServiceCollection services)
//     {
//         services.AddSwaggerGen(c =>
//         {
//             c.AddSecurityDefinition("X-USER-ID", new OpenApiSecurityScheme
//             {
//                 Name = "X-USER-ID",
//                 Type = SecuritySchemeType.ApiKey,
//                 In = ParameterLocation.Header,
//                 Description = "User ID header"
//             });
//             c.AddSecurityRequirement(new OpenApiSecurityRequirement
//             {
//                 {
//                     new OpenApiSecurityScheme
//                     {
//                         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "X-USER-ID" }
//                     },
//                     []
//                 }
//             });
//         });

//         return services;
//     }
// }