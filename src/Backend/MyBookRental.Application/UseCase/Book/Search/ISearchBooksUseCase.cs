using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.UseCase.Book.Search
{
    public interface ISearchBooksUseCase
    {
        Task<IList<ResponseSearchedBookJson>> Execute(string? title, string? author, string? isbn);
    }
}
