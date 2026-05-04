using FluentValidation;
using mini_shop_backend_net.Application.DTOs.Category;

namespace mini_shop_backend_net.Application.Validators;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название обязательно")
            .MaximumLength(100);
    }
}