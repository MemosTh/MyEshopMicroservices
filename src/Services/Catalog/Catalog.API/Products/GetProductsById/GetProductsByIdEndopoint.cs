using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProductsById;


public record GetProductRequest(Guid guid);


public record GetProductResponse(Product Product);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{guid}", async (Guid guid, ISender sender) =>
        {
           
            var result = await sender.Send(new GetProductQuery(guid));

            var response = result.Adapt<GetProductResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}