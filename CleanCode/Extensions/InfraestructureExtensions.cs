
using CleanCode.Application.Handlers;
using CleanCode.Application.Mapping.Profiles;
using CleanCode.Infraestructura.Data;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfraestructureExtensions
    {
        public static IServiceCollection AddInfraestructuraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<CleanCodeDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddAutoMapper(typeof(ClienteProfile).Assembly)
                .AddApplicationInsightsTelemetry()
                .AddSwaggerServices(configuration)
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
                .AddSwaggerServices(configuration)
               .AddCors(options =>
               {
                   options.AddPolicy("MyExposeResponseHeadersPolicy",
                       policy =>
                       {
                           policy.WithExposedHeaders("x-pagination-total");
                       });
               })
                .AddHealthChecks()
                ;



            services.Configure<RequestLocalizationOptions>(options =>
            options.DefaultRequestCulture = new RequestCulture(culture: "es-CL", uiCulture: "es-CL")
            );

            return services;
        }
    }
}
