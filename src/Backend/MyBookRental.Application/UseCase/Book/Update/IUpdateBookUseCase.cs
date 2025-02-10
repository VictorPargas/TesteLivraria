using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.UseCase.Book.Update
{
    public  interface IUpdateBookUseCase
    {
        Task<ResponseRegisteredBookJson> Execute(int id, RequestUpdateBookJson request);
    }
}
