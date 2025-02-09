namespace MyBookRental.Communication.Requests
{
    public class RequestBookJson
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int YearPublished { get; set; }
        public int QuantityAvailable { get; set; }
        public long PublisherId { get; set; }
        public IList<long> AuthorIds { get; set; } = new List<long>();
    }
}
