namespace MyBookRental.Domain.Entities
{
    public class BookRental
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = "Pendente";
        public int RenewalsCount { get; set; } = 0;

        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}
