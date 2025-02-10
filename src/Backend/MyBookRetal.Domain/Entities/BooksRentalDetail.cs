namespace MyBookRental.Domain.Entities
{
    public class BooksRentalDetail : EntityBase
    {
        public long RentalId { get; set; }
        public long BookId { get; set; }
        public int Quantity { get; set; }

        // Propriedades de navegação
        public BookRental BooksRental { get; set; }
        public Book Book { get; set; }
    }
}
