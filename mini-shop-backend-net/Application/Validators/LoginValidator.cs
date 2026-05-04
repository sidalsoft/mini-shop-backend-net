using mini_shop_backend_net.Application.DTOs.User;

namespace mini_shop_backend_net.Application.Validators;

using FluentValidation;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}