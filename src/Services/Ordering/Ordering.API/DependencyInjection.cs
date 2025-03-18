using Carter;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCarter();

        return serviceCollection;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();

        return app;
    }
}
