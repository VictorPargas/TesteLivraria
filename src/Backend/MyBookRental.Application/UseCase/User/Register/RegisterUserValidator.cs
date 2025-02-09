using FluentValidation;
using MyBookRental.Application.SharedValidators;
using MyBookRental.Communication.Requests;
using MyBookRental.Excepetion;
namespace MyBookRental.Application.UseCase.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessage.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessage.EMAIL_EMPTY).EmailAddress();
            RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
            RuleFor(user => user.Phone).NotEmpty().Matches(@"^\+?[1-9]\d{1,14}$");
            When(user => string.IsNullOrEmpty(user.Email) == false, () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessage.EMAIL_INVALID);
            });
        }
    }
}
