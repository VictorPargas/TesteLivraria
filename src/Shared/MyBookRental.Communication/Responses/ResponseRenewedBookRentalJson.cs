namespace MyBookRental.Communication.Responses
{
    public class ResponseRenewedBookRentalJson
    {
        public long RentalId { get; set; }
        public DateTime NewDueDate { get; set; }
        public int RenewalsCount { get; set; }
        public string Status { get; set; }
    }
}
