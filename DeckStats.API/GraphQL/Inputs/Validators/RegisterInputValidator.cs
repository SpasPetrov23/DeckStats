using FluentValidation;
using ErrorCodes = DeckStats.Common.Errors.ErrorCodes;

namespace DeckStats.API.GraphQL.Inputs.Validators;

public class RegisterInputValidator : AbstractValidator<RegisterInput>
{
    public RegisterInputValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty().WithErrorCode(ErrorCodes.NOT_EMPTY_FIELD.ToString())
            .MinimumLength(3).WithErrorCode(ErrorCodes.USERNAME_MIN_LENGTH.ToString())
            .MaximumLength(25).WithErrorCode(ErrorCodes.USERNAME_MAX_LENGTH.ToString());
        
        RuleFor(r => r.Email)
            .NotEmpty().WithErrorCode(ErrorCodes.NOT_EMPTY_FIELD.ToString())
            .EmailAddress().WithErrorCode(ErrorCodes.INVALID_EMAIL_ADDRESS.ToString());
        
        RuleFor(r => r.Password)
            .NotEmpty().WithErrorCode(ErrorCodes.NOT_EMPTY_FIELD.ToString())
            .MinimumLength(8).WithErrorCode(ErrorCodes.PASSWORD_MINIMUM_LENGHT.ToString())
            .Matches("[a-z]").WithErrorCode(ErrorCodes.PASSWORD_MUST_MATCH_LOWER_CASE.ToString())
            .Matches("[0-9]").WithErrorCode(ErrorCodes.PASSWORD_MUST_MATCH_NUMBER.ToString());
    }
}
