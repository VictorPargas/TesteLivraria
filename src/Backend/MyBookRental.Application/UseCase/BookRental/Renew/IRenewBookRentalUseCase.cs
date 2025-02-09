using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.UseCase.BookRental.Renew
{
    public interface IRenewBookRentalUseCase
    {
        Task<ResponseRenewedBookRentalJson> Execute(RequestRenewBookRentalJson request);
    }
}
