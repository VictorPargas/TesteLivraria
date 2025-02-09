using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.UseCase.Book.Get
{
    public interface IGetBooksUseCase
    {
        Task<IList<ResponseRegisteredBookJson>> Execute();
    }
}
