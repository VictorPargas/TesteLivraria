using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyBookRental.Communication.Requests;

namespace MyBookRental.Application.UseCase.Book.Register
{
    public class RegisterBookValidator : AbstractValidator<RequestRegisterBookJson>
    {
        public RegisterBookValidator()
        {
            RuleFor(book => book.Title).NotEmpty().WithMessage("O título não pode ser vazio!");
            RuleFor(book => book.Author).NotEmpty().WithMessage("O autor não pode ser vazio!");
            RuleFor(book => book.ISBN).NotEmpty().WithMessage("O ISBN não pode ser vazio!").Length(13).WithMessage("O ISBN deve ter 13 caracteres.");
            RuleFor(book => book.Quantity).GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
            RuleFor(book => book.Year).InclusiveBetween(1500, DateTime.Now.Year).WithMessage("Ano inválido.");
        }
    }
}
