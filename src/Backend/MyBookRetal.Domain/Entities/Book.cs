using System.ComponentModel.DataAnnotations.Schema;

namespace MyBookRental.Domain.Entities
{
    public class Book : EntityBase
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string YearPublished { get; set; } = string.Empty;
        public int QuantityAvailable { get; set; } 
        public long PublisherId { get; set; }

        // Propriedade de navegação para acessar os dados da editora
        public Publisher Publisher { get; set; }

        public IList<BookAuthor> BookAuthors { get; set; } = [];
    }
}
