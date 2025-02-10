using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookRental.Domain.Entities
{
    [Table("BooksAuthors")]
    public class BookAuthor : EntityBase
    {
        public long BookId { get; set; }
        public Book Book { get; set; }

        public long AuthorId { get; set; }
        public Author Author { get; set; }


    }
}
