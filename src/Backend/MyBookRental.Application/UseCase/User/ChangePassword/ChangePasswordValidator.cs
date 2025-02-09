using FluentValidation;
using MyBookRental.Application.SharedValidators;
using MyBookRental.Communication.Requests;

namespace MyBookRental.Application.UseCase.User.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.Password).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
        }
    }
}
