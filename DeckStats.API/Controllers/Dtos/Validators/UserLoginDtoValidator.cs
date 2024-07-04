using DeckStats.API.Handlers.UserHandlers.Dtos;
using FluentValidation;
using ErrorCodes = DeckStats.API.Utils.ExceptionHandling.ErrorCodes;

namespace DeckStats.API.Controllers.Dtos.Validators;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithErrorCode(ErrorCodes.NOT_EMPTY_FIELD.ToString())
            .EmailAddress().WithErrorCode(ErrorCodes.INVALID_EMAIL_ADDRESS.ToString());
    }
}
