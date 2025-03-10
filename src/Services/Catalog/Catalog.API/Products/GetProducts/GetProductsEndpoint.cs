namespace Catalog.API.Products.GetProducts;

//public record GetProductsRequest();

public record GetProductsResponce(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",
            async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());

                var responce = result.Adapt<GetProductsResponce>();

                return Results.Ok(responce);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponce>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetProducts")
            .WithDescription("GetProducts");
    }
}
