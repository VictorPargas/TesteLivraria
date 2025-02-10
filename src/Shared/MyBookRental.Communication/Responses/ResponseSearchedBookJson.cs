namespace MyBookRental.Communication.Responses
{
    public class ResponseSearchedBookJson
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int Year { get; set; }
        public int QuantityAvailable { get; set; }
        public List<string> Authors { get; set; } = new List<string>();
    }
}
