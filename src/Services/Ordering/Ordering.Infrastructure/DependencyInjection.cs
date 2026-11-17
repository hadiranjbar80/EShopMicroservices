using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration) 
        {

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, opts) =>
            {
                opts.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                opts.UseSqlServer(configuration.GetConnectionString("OrderingConnection"));
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
