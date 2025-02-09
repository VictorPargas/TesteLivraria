using Microsoft.EntityFrameworkCore;
using MyBookRental.Domain.Entities;
using MyBookRental.Domain.Repositories.Publisher;

namespace MyBookRental.Infrastructure.DataAccess.Repositories
{
    public class PublisherRepository : IPublisherWriteOnlyRepository, IPublisherReadOnlyRepository
    {
        private readonly MyBookRentalDbContext _dbContext;

        public PublisherRepository(MyBookRentalDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Publisher publisher) => await _dbContext.Publishers.AddAsync(publisher);

        public async Task<bool> ExistsPublisher(long publisherId)
        {
            return await _dbContext.Publishers.AnyAsync(p => p.Id == publisherId);
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishers()
        {
            return await _dbContext.Publishers.ToListAsync();
        }
    }
}
