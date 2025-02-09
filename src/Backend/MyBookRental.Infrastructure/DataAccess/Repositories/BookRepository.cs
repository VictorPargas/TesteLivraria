﻿using Microsoft.EntityFrameworkCore;
using MyBookRental.Domain.Entities;
using MyBookRental.Domain.Repositories.Book;

namespace MyBookRental.Infrastructure.DataAccess.Repositories
{
    public class BookRepository : IBookReadOnlyRepository, IBookWriteOnlyRepository
    {
        private readonly MyBookRentalDbContext _dbContext;

        public BookRepository(MyBookRentalDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Book book) => await _dbContext.Books.AddAsync(book);

        public async Task<Book> GetBookWithDetails(long bookId)
        {
            return await _dbContext.Books
                .Include(b => b.Publisher) // Inclui a editora
                .Include(b => b.BookAuthors) // Inclui a relação de autores
                    .ThenInclude(ba => ba.Author) // Inclui os dados dos autores
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }
        public async Task<IList<Book>> GetAllBooksWithDetails()
        {
            return await _dbContext.Books
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .ToListAsync();
        }

        public async Task<bool> ExistsBookWithISBN(string isbn)
        {
            return await _dbContext.Books.AnyAsync(b => b.ISBN == isbn);
        }

        public async Task<bool> ExistsPublisher(long publisherId)
        {
            return await _dbContext.Publishers.AnyAsync(p => p.Id == publisherId);
        }
    }
}
