namespace MyBookRental.Communication.Responses
{
    public class ResponseRegisteredBookJson
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int YearPublished { get; set; }
        public int QuantityAvailable { get; set; } 

        public string PublisherName { get; set; } = string.Empty;

        public IList<string> Authors { get; set; } = new List<string>();
    }
}
