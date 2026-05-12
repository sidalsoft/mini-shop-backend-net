using FluentValidation;
using miniShopBackendNet.Application.DTOs.Category;

namespace miniShopBackendNet.Application.Validators;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название обязательно")
            .MaximumLength(100);
    }
}