using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookRental.Domain.Repositories.Publisher
{
    public interface IPublisherReadOnlyRepository
    {
        Task<IEnumerable<Entities.Publisher>> GetAllPublishers();
        Task<bool> ExistsPublisher(long publisherId);
    }
}
