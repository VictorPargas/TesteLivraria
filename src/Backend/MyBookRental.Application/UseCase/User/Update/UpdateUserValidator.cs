using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyBookRental.Communication.Requests;
using MyBookRental.Exceptions;

namespace MyBookRental.Application.UseCase.User.Update
{
    public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
    {
        public UpdateUserValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMessage.NAME_EMPTY);
            RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMessage.EMAIL_EMPTY);
            // tem que fazer o rule sobre o perfil do usuario 
            When(request => string.IsNullOrWhiteSpace(request.Email) == false, () =>
            {
                RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMessage.EMAIL_INVALID);
            });
        }
    }
}
