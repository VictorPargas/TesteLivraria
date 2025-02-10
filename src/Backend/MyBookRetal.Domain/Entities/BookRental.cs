namespace MyBookRental.Domain.Entities
{
    public class BookRental : EntityBase
    {
        public long BookId { get; set; }
        public long UserId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public decimal? Fine { get; set; }

        public string Status { get; set; } = "Pendente";

        // Propriedades de navegação
        public Book Book { get; set; }
        public User User { get; set; }
        public IList<BooksRentalDetail> RentalDetails { get; set; } = [];
    }
}
