namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Categories,
    string Description, string ImageFile, decimal Price);

public record CreateProductResponce(Guid Id);


public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
            async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);
                var responce = result.Adapt<CreateProductResponce>();
                return Results.Created($"/products/{responce.Id}", responce);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponce>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
    }
}
