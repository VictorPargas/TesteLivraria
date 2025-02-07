namespace MyBookRental.Communication.Requests
{
    public class RequestRegisterBookRentalJson
    {
        public long UserId { get; set; }
        public long BookId { get; set; }
        public DateTime DueDate { get; set; } // Data prevista para devolução
    }
}
