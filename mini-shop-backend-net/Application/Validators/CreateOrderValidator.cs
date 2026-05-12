using miniShopBackendNet.Application.DTOs.Order;

namespace miniShopBackendNet.Application.Validators;

using FluentValidation;

public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя обязательно")
            .MaximumLength(100);

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Телефон обязателен")
            .Matches(@"^\+?[0-9]{9,15}$")
            .WithMessage("Некорректный номер телефона");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Адрес обязателен")
            .MaximumLength(300);
    }
}