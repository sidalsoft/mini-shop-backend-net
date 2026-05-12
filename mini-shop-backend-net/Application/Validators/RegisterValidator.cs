using miniShopBackendNet.Application.DTOs.User;

namespace miniShopBackendNet.Application.Validators;

using FluentValidation;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email обязателен")
            .EmailAddress().WithMessage("Некорректный email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен")
            .MinimumLength(6).WithMessage("Минимум 6 символов")
            .Matches("[A-Z]").WithMessage("Должна быть заглавная буква")
            .Matches("[0-9]").WithMessage("Должна быть цифра");
    }
}