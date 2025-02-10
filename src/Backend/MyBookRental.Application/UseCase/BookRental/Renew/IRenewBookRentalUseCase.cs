using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.UseCase.BookRental.Renew
{
    public interface IRenewBookRentalUseCase
    {
        Task Execute(long rentalId, RequestRenewBookRentalJson request);
    }
}
