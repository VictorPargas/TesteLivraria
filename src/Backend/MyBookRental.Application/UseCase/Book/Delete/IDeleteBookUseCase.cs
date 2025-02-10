namespace MyBookRental.Application.UseCase.Book.Delete
{
    public interface IDeleteBookUseCase
    {
        Task Execute(int id);
    }
}
