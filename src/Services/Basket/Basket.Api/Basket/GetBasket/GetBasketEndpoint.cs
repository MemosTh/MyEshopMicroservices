﻿using Carter;
using Mapster;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace Basket.Api.Basket.GetBasket;

public record GetBasketRequest();

public record GetBasketResponse(ShoppingCart ShoppingCart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));
            
            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket")
        .WithDescription("Basket");
    }
}
