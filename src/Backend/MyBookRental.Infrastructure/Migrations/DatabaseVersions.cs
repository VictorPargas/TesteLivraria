using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookRental.Infrastructure.Migrations
{
    public abstract class DatabaseVersions
    {
        public const int TABLE_USER = 1;
        public const int TABLE_BOOKS = 2;
    }
}
