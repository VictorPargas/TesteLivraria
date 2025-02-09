using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBookRental.Domain.Entities;
using MyBookRental.Domain.Repositories.Author;

namespace MyBookRental.Infrastructure.DataAccess.Repositories
{
    public class AuthorRepository : IAuthorWriteOnlyRepository, IAuthorReadOnlyRepository
    {
        private readonly MyBookRentalDbContext _dbContext;

        public AuthorRepository(MyBookRentalDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Author author) => await _dbContext.Authors.AddAsync(author);

        public async Task<bool> ExistsAuthor(long authorId)
        {
            return await _dbContext.Authors.AnyAsync(a => a.Id == authorId);
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _dbContext.Authors.ToListAsync();
        }
    }
}
