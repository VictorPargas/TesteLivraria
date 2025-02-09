namespace MyBookRental.Domain.Repositories.Author
{
    public interface IAuthorWriteOnlyRepository
    {
        Task Add(Entities.Author author);
    }
}
