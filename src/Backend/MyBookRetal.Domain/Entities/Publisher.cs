using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookRental.Domain.Entities
{
    public class Publisher : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public IList<Book> Books { get; set; } = [];
    }
}
