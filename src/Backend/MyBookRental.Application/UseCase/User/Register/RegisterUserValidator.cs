using FluentValidation;
using MyBookRental.Communication.Requests;
using MyBookRental.Excepetion;
namespace MyBookRental.Application.UseCase.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessage.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().WithMessage("O email não pode ser vazio!").EmailAddress();
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6);
            RuleFor(user => user.Phone).NotEmpty().Matches(@"^\+?[1-9]\d{1,14}$");
            RuleFor(User => User.Profile).NotEmpty().Must(type => type == "Administrador" || type == "Usuário");
        }
    }
}
