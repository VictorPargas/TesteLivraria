using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookRental.Communication.Requests
{
    public class RequestBookAuthorJson
    {
        public long BookId { get; set; }
        public long AuthorId { get; set; }
    }
}
