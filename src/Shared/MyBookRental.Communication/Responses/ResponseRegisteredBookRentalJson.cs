﻿namespace MyBookRental.Communication.Responses
{
    public class ResponseRegisteredBookRentalJson
    {
        public long RentalId { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public string Status { get; set; } = string.Empty;

        public Guid UserIdentifier { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
