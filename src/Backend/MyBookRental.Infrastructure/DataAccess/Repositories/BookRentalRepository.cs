using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBookRental.Domain.Entities;
using MyBookRental.Domain.Repositories.BookRental;

namespace MyBookRental.Infrastructure.DataAccess.Repositories
{
    public class BookRentalRepository : IBookRentalReadOnlyRepository, IBookRentalWriteOnlyRepository
    {
        private readonly MyBookRentalDbContext _dbContext;

        public BookRentalRepository(MyBookRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task Add(BookRental bookRental)
        {
            await _dbContext.BookRentals.AddAsync(bookRental);
        }

        public async Task Update(BookRental bookRental)
        {
            _dbContext.BookRentals.Update(bookRental);
        }

        public async Task<IEnumerable<BookRental>> GetAllRentals()
        {
            return await _dbContext.BookRentals
                .Include(br => br.User)  // Inclui o usuário associado à locação
                .Include(br => br.Book)  // Inclui o livro associado à locação
                .ToListAsync();
        }

        public async Task<BookRental?> GetRentalById(long rentalId)
        {
            return await _dbContext.BookRentals
                //.Include(br => br.User)
                //.Include(br => br.Book)
                .FirstOrDefaultAsync(br => br.Id == rentalId);
        }

        public async Task<bool> IsBookAvailable(long bookId)
        {
            var rentedCount = await _dbContext.BookRentals
                .CountAsync(br => br.BookId == bookId && br.Status == "Pendente");

            var book = await _dbContext.Books.FindAsync(bookId);
            return book != null && rentedCount < book.Quantity;
        }
    }
}
