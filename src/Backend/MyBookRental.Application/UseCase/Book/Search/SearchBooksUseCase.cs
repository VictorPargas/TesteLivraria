using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.Book;

namespace MyBookRental.Application.UseCase.Book.Search
{
    public class SearchBooksUseCase : ISearchBooksUseCase
    {
        private readonly IBookReadOnlyRepository _readOnlyRepository;

        public SearchBooksUseCase(IBookReadOnlyRepository readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
        }

        public async Task<IList<ResponseSearchedBookJson>> Execute(string? title, string? author, string? isbn)
        {
            var books = await _readOnlyRepository.SearchBooks(title, author, isbn);

            return books.Select(book => new ResponseSearchedBookJson
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                Year = int.Parse(book.YearPublished),
                QuantityAvailable = book.QuantityAvailable,
                Publisher = book.Publisher?.Name ?? "Desconhecida",
                Authors = book.BookAuthors.Select(ba => ba.Author.Name).ToList()
            }).ToList();
        }
    }
}
