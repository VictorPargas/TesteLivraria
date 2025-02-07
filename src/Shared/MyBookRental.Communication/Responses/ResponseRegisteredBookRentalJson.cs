namespace MyBookRental.Communication.Responses
{
    public class ResponseRegisteredBookRentalJson
    {
        public long RentalId { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public string Status { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
