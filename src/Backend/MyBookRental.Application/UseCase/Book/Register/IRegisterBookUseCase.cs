using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.UseCase.Book.Register
{
    public interface IRegisterBookUseCase
    {
        Task<ResponseRegisteredBookJson> Execute(RequestRegisterBookJson request);
    }
}
