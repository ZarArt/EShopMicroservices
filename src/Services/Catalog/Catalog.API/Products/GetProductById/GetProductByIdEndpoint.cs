namespace Catalog.API.Products.GetProductById;

//public record GetProductByIdRequest;

public record GetProductByIdResponce(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}",
            async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));

                var responce = result.Adapt<GetProductByIdResponce>();

                return Results.Ok(responce);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponce>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by Id")
            .WithDescription("Get Product by Id");
    }
}
