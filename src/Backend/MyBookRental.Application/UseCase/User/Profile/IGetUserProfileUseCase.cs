using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.UseCase.User.Profile
{
    public interface IGetUserProfileUseCase
    {
        public Task<ResponseUserProfileJson> Execute();
    }
}
