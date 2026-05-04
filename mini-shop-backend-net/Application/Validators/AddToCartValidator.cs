using mini_shop_backend_net.Application.DTOs.Cart;

namespace mini_shop_backend_net.Application.Validators;

using FluentValidation;

public class AddToCartValidator : AbstractValidator<AddToCartDto>
{
    public AddToCartValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Количество должно быть > 0")
            .LessThanOrEqualTo(100).WithMessage("Слишком большое количество");
    }
}