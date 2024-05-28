

using Basket.API.Data;

namespace Basket.Api.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart ShoppingCart);


public class GetBasketqQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
       var shoppingCart =  await basketRepository.GetBasket(request.UserName, cancellationToken);

        return new GetBasketResult(shoppingCart);
    }
}
