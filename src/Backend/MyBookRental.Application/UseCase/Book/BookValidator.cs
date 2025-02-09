using FluentValidation;
using MyBookRental.Communication.Requests;
using MyBookRental.Domain.Entities;

namespace MyBookRental.Application.UseCase.Book
{
    public class BookValidator : AbstractValidator<RequestBookJson>
    {
       public BookValidator()
        {
            RuleFor(book => book.Title)
               .NotEmpty().WithMessage("O título do livro é obrigatório.")
               .MaximumLength(255).WithMessage("O título do livro deve ter no máximo 255 caracteres.");

            RuleFor(book => book.ISBN)
               .NotEmpty().WithMessage("O ISBN é obrigatório.")
               .Length(10, 13).WithMessage("O ISBN deve ter entre 10 e 13 caracteres.");

            RuleFor(book => book.YearPublished)
                .InclusiveBetween(1450, DateTime.Now.Year)
                .WithMessage($"O ano de publicação deve estar entre 1450 e {DateTime.Now.Year}.");

            RuleFor(book => book.QuantityAvailable)
              .GreaterThanOrEqualTo(0).WithMessage("A quantidade disponível deve ser maior ou igual a zero.");

            RuleFor(book => book.PublisherId)
               .GreaterThan(0).WithMessage("O ID da editora é obrigatório e deve ser maior que zero.");

            RuleFor(book => book.AuthorIds)
                .NotEmpty().WithMessage("Pelo menos um autor deve ser informado.")
                .Must(authors => authors.All(id => id > 0))
                .WithMessage("Todos os IDs dos autores devem ser maiores que zero.");
        }
    }
}
