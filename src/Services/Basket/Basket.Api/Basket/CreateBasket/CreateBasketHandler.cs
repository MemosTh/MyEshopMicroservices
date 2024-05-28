using Basket.API.Data;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.Api.Basket.CreateBasket;

public record CreateBasketCommand(ShoppingCart ShoppingCart) : ICommand<CreateBasketResult>;
public record CreateBasketResult(bool IsSuccess);

public class StoreBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}


public class CreateBasketHandler(IBasketRepository basketRepository) : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    

    public async Task<CreateBasketResult> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        await basketRepository.StoreBasket(request.ShoppingCart, cancellationToken);

        return new CreateBasketResult(true);
    }
}
