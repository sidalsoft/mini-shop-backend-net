using mini_shop_backend_net.Application.DTOs;

namespace mini_shop_backend_net.Application.Validators;

using FluentValidation;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название обязательно")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Цена должна быть больше 0");

        RuleFor(x => x.ImageUrl)
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Некорректный URL")
            .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));
        
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Категория обязательна");
    }
}