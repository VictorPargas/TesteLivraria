namespace MyBookRental.Communication.Responses
{
    public class ResponseRegisteredBookJson
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
    }
}
