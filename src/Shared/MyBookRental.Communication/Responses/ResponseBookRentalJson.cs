namespace MyBookRental.Communication.Responses
{
    public class ResponseBookRentalJson
    {
        public long RentalId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public DateTime RentalDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }

        public Guid UserIdentifier { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
