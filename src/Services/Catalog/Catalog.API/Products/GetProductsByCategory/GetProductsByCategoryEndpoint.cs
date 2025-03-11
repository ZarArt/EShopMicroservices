
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductsByCategory;

//public record GetProductsByCategoryRequest();

public record GetProductsByCategoryResponce(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
            async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsByCategoryQuery(category));

                var responce = result.Adapt<GetProductsByCategoryResponce>();

                return Results.Ok(responce);
            })
            .WithName("GetProductsByCategory")
            .Produces<GetProductsByCategoryResponce>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products by Category")
            .WithDescription("Get Products by Category");
    }
}
