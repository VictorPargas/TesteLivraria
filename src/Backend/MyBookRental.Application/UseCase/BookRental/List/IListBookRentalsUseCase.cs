using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.UseCase.BookRental.List
{
    public interface IListBookRentalsUseCase
    {
        Task<IEnumerable<ResponseRegisteredBookRentalJson>> Execute();
        Task<IEnumerable<ResponseRegisteredBookRentalJson>> ExecuteForCurrentUser();

    }
}
