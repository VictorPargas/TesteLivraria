namespace MyBookRental.Domain.Repositories.BookRental
{
    public interface IBookRentalReadOnlyRepository
    {
        Task<IEnumerable<Domain.Entities.BookRental>> GetAllRentals();
        Task<Domain.Entities.BookRental?> GetRentalById(long rentalId);
        Task<bool> IsBookAvailable(long bookId);
    }

    public interface IBookRentalWriteOnlyRepository
    {
        Task Add(Domain.Entities.BookRental bookRental);
        Task Update(Domain.Entities.BookRental bookRental);
    }
}
