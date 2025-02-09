using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.UseCase.Publisher
{
    public interface IRegisterPublisherUseCase
    {
        Task<ResponseRegisteredPublisherJson> Execute(RequestPublisherJson request);
    }
}
