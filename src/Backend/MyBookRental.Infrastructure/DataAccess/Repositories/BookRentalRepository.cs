using Microsoft.EntityFrameworkCore;
using MyBookRental.Domain.Entities;
using MyBookRental.Domain.Repositories.BookRental;

namespace MyBookRental.Infrastructure.DataAccess.Repositories
{
    public class BookRentalRepository(MyBookRentalDbContext context) : IBookRentalReadOnlyRepository, IBookRentalWriteOnlyRepository
    {
        private readonly MyBookRentalDbContext _context = context;

        public async Task Add(BookRental bookRental)
        {
            await _context.BooksRental.AddAsync(bookRental);

            var book = await _context.Books.FindAsync(bookRental.BookId);
            if (book != null)
            {
                book.QuantityAvailable--;
                _context.Books.Update(book);
            }
        }

        public async Task<IEnumerable<BookRental>> GetRentalsByUserIdentifier(Guid userIdentifier)
        {
            return await _context.BooksRental
                .Include(r => r.Book)
                .Include(r => r.User)
                .Where(r => r.User.UserIdentifier == userIdentifier)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookRental>> GetAllRentals()
        {
            return await _context.BooksRental
                     .Include(r => r.Book)
                     .Include(r => r.User) // Inclui os dados do usuário, incluindo UserIdentifier
                     .ToListAsync();
        }

        public async Task<BookRental?> GetRentalById(long rentalId)
        {
            return await _context.BooksRental
                    .Include(r => r.Book)
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.Id == rentalId);
        }

        public async Task<bool> IsBookAvailable(long bookId)
        {
            var book = await _context.Books.FindAsync(bookId);

            if (book == null)
                return false;

            if (!book.Active) // Verifica se o livro está inativo
                return false;

            if (book.QuantityAvailable <= 0)
                return false;

            int totalRented = await _context.BooksRental
                .CountAsync(r => r.BookId == bookId && r.ActualReturnDate == null);

            // Se o número de cópias alugadas for igual ou superior à quantidade disponível, o livro não está disponível
            if (totalRented >= book.QuantityAvailable)
                return false;

            return true;
        }

        public async Task Update(BookRental bookRental)
        {
            _context.BooksRental.Update(bookRental);
            await Task.CompletedTask;
        }       
    }
}
