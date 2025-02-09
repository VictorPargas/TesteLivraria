using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookRental.Domain.Entities
{
    public class Author : EntityBase
    {
        public string Name { get; set; } = string.Empty;

        public IList<BookAuthor> BookAuthors { get; set; } = [];
    }
}
