using Microsoft.EntityFrameworkCore;
using MyBookRental.Domain.Entities;
using MyBookRental.Domain.Repositories.User;

namespace MyBookRental.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository, IUserDeleteOnlyRepository
    {
        private readonly MyBookRentalDbContext _dbContenxt;

        public UserRepository(MyBookRentalDbContext dbContext) => _dbContenxt = dbContext;

        public async Task Add(User user) => await _dbContenxt.Users.AddAsync(user);

        public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContenxt.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);

        public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) => await _dbContenxt.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);
        public async Task DeleteAccount(Guid userIdentifier)
        {
            var user = await _dbContenxt.Users.FirstOrDefaultAsync(u => u.UserIdentifier == userIdentifier);

            if (user is null)
                return;

            var rentals = _dbContenxt.BooksRental.Where(r => r.UserId == user.Id);

            _dbContenxt.BooksRental.RemoveRange(rentals);
            _dbContenxt.Users.Remove(user);

            await _dbContenxt.SaveChangesAsync();  // Garantir que as alterações são persistidas
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _dbContenxt
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email));
        }

        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
            return await _dbContenxt
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email) && user.Password.Equals(password));
        }

        public async Task<User> GetById(long id)
        {
            return await _dbContenxt
                .Users
                .FirstAsync(user => user.Id == id);
        }

        public void Update(User user) => _dbContenxt.Users.Update(user);

        public async Task<User?> GetUserByIdentifierAsync(Guid userIdentifier)
        {
            return await _dbContenxt.Users.FirstOrDefaultAsync(u => u.UserIdentifier == userIdentifier);
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContenxt.Users.ToListAsync();
        }

        public async Task<bool> HasRentals(long userId)
        {
            return await _dbContenxt.BooksRental.AnyAsync(r => r.UserId == userId);
        }
    }
}
