namespace MyBookRental.Communication.Responses
{
    public class ResponseReturnedBookRentalJson
    {
        public long RentalId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; }
        public decimal LateFee { get; set; }
    }
}
