using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBookRental.Domain.Entities;
using MyBookRental.Domain.Repositories.Book;

namespace MyBookRental.Infrastructure.DataAccess.Repositories
{
    public class BookRepository : IBookReadOnlyRepository, IBookWriteOnlyRepository
    {
        private readonly MyBookRentalDbContext _dbContext;

        public BookRepository(MyBookRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Book book)
        {
            await _dbContext.Books.AddAsync(book);
        }

        public async Task<bool> ExistsBookWithISBN(string isbn)
        {
            return await _dbContext.Books.AnyAsync(b => b.ISBN == isbn);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _dbContext.Books.ToListAsync();
        }
    }
}
