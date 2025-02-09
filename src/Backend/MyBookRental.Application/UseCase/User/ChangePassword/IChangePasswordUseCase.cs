using MyBookRental.Communication.Requests;

namespace MyBookRental.Application.UseCase.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task Execute(RequestChangePasswordJson request);
    }
}
