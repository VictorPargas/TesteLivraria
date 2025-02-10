using MyBookRental.Domain.Repositories.Book;
using MyBookRental.Domain.Repositories;
using MyBookRental.Exceptions.ExceptionsBase;

namespace MyBookRental.Application.UseCase.Book.Delete
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IBookWriteOnlyRepository _writeOnlyRepository;
        private readonly IBookReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookUseCase(
            IBookWriteOnlyRepository writeOnlyRepository,
            IBookReadOnlyRepository readOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(int id)
        {
            // Recupera o livro com detalhes (alugueis e autores)
            var book = await _readOnlyRepository.GetBookWithDetails(id);

            if (book == null)
                throw new ErrorOnValidationException(new List<string> { "Livro não encontrado." });

            // Verifica se o livro está associado a aluguéis antes de deletar
            if (book.BookRentals != null && book.BookRentals.Any())
            {
                throw new ErrorOnValidationException(new List<string>
                {
                    "Não é possível deletar o livro. Existem aluguéis associados a este livro."
                });
            }

            // Verifica se o livro tem autores associados e remove as associações
            if (book.BookAuthors != null && book.BookAuthors.Any())
            {
                book.BookAuthors.Clear();  // Remove as associações com os autores
            }

            // Deleta o livro após verificar as dependências
            await _writeOnlyRepository.Delete(book);
            await _unitOfWork.Commit();
        }
    }
}
