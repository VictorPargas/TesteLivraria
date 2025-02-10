using FluentValidation;
using MyBookRental.Communication.Requests;

namespace MyBookRental.Application.UseCase.Book.Update
{
    public class UpdateBookValidator : AbstractValidator<RequestUpdateBookJson>
    {
        public UpdateBookValidator()
        {
            When(book => book.AuthorIds != null, () =>
            {
                RuleFor(book => book.AuthorIds)
                    .Must(ids => ids.Distinct().Count() == ids.Count) // Evita duplicatas
                    .WithMessage("A lista de autores contém IDs duplicados.")
                    .Must(ids => ids.All(id => id > 0))
                    .WithMessage("Todos os IDs de autores devem ser válidos (maiores que zero).");
            });
        }
    }
}
