namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection serviceCollection)
    {
        //addCarter()

        return serviceCollection;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        //app.UseCarter()

        return app;
    }
}
