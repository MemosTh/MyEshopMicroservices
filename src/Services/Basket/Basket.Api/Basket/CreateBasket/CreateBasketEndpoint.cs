using Carter;
using Mapster;

namespace Basket.Api.Basket.CreateBasket;

public record CreateBasketRequets(ShoppingCart ShoppingCart);
public record CreateBasketResponse(string Username );

public class CreateBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (CreateBasketRequets request, ISender sender) => 
        {
            var command = request.Adapt<CreateBasketCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateBasketResponse>();

            return Results.Created($"/basket/{request.ShoppingCart.UserName}", response);
        })
        .WithName("CreateBasket")
        .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Basket")
        .WithDescription("Create Basket");
    }
}
