namespace MyBookRental.Domain.Repositories.Book
{
    public interface IBookReadOnlyRepository
    {
        Task<bool> ExistsBookWithISBN(string isbn);
        Task<bool> ExistsPublisher(long publisherId);

        Task<Domain.Entities.Book> GetBookWithDetails(long bookId);

        Task<IList<Entities.Book>> GetAllBooksWithDetails();
    }

    public interface IBookWriteOnlyRepository
    {
        Task Add(Entities.Book book);
    }
}
