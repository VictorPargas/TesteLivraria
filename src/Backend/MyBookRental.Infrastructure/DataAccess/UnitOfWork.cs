using MyBookRental.Domain.Repositories;

namespace MyBookRental.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyBookRentalDbContext _dbContext;

        public UnitOfWork(MyBookRentalDbContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
