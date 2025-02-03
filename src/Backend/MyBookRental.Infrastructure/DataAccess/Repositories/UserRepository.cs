using Microsoft.EntityFrameworkCore;
using MyBookRental.Domain.Entities;
using MyBookRental.Domain.Repositories.User;

namespace MyBookRental.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly MyBookRentalDbContext _dbContenxt;

        public UserRepository(MyBookRentalDbContext dbContext) => _dbContenxt = dbContext;

        public async Task Add(User user) => await _dbContenxt.Users.AddAsync(user);

        public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContenxt.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
    }
}
