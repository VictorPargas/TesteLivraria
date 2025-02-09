using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.UseCase.User.List
{
    public interface IListUsersUseCase
    {
        Task<IEnumerable<ResponseUserJson>> Execute();
    }
}
