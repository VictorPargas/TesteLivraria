namespace MyBookRental.Application.UseCase.User.Delete
{
    public interface IDeleteUserUseCase
    {
        Task Execute();
        Task Execute(Guid userIdentifier);
    }
}
