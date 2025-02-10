namespace MyBookRental.Domain.Repositories.Book
{
    public interface IBookReadOnlyRepository
    {
        Task<bool> ExistsBookWithISBN(string isbn);
        Task<bool> ExistsPublisher(long publisherId);
        Task<Domain.Entities.Book> GetBookWithDetails(long bookId);
        Task<IList<Entities.Book>> GetAllBooksWithDetails();

        Task<IList<Entities.Book>> SearchBooks(string? title, string? author, string? isbn);
    }

    public interface IBookWriteOnlyRepository
    {
        Task Add(Entities.Book book);
        Task Update(Entities.Book book);
        Task Delete(Entities.Book book);
    }
}
