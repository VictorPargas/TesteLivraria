namespace MyBookRental.Domain.Repositories.Author
{
    public interface IAuthorReadOnlyRepository
    {
        Task<IEnumerable<Entities.Author>> GetAllAuthors();
        Task<bool> ExistsAuthor(long authorId);
    }
}
