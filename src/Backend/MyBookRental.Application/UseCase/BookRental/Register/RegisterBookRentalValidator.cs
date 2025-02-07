using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyBookRental.Communication.Requests;

namespace MyBookRental.Application.UseCase.BookRental.Register
{
    public class RegisterBookRentalValidator : AbstractValidator<RequestRegisterBookRentalJson>
    {
        public RegisterBookRentalValidator()
        {
            RuleFor(rental => rental.UserId).NotEmpty().WithMessage("O ID do usuário é obrigatório.");
            RuleFor(rental => rental.BookId).NotEmpty().WithMessage("O ID do livro é obrigatório.");
            RuleFor(rental => rental.DueDate).GreaterThan(DateTime.UtcNow).WithMessage("A data de devolução prevista deve ser no futuro.");
        }
    }
}
