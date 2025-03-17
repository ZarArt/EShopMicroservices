using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        serviceCollection.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        serviceCollection.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptors>();

        serviceCollection.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });

        //serviceCollection.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return serviceCollection;
    }
}
