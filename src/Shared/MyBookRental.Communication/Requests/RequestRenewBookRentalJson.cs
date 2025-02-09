namespace MyBookRental.Communication.Requests
{
    public class RequestRenewBookRentalJson
    {
        public long RentalId { get; set; }              
        public DateTime NewDueDate { get; set; }
    }
}
