namespace Catalog.API.Products.DeleteProduct;

//public record DeleteProductRequest;

public record DeleteProductResponce(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}",
            async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));

                var responce = result.Adapt<DeleteProductResponce>();

                return Results.Ok(responce);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponce>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
    }
}
