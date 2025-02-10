namespace MyBookRental.Communication.Requests
{
    public class RequestUpdateBookJson
    {
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public int? Quantity { get; set; }
        public int? Year { get; set; }
        public string? Publisher { get; set; }

        // Lista de IDs dos autores relacionados ao livro
        public List<long>? AuthorIds { get; set; }
    }
}
