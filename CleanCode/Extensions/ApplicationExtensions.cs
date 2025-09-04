using CleanCode.Application.Interfaces;
using CleanCode.Application.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddScoped<IClienteServicio, ClienteServicio>();

            return services;
        }
    }
}
