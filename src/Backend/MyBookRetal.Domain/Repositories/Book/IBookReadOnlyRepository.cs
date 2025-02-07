using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookRental.Domain.Repositories.Book
{
    public interface IBookReadOnlyRepository
    {
        Task<bool> ExistsBookWithISBN(string isbn);
        Task<IEnumerable<Entities.Book>> GetAllBooks();
    }

    public interface IBookWriteOnlyRepository
    {
        Task Add(Entities.Book book);
    }
}
