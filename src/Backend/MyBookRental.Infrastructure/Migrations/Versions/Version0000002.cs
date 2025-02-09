using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace MyBookRental.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_BOOKS, "Create table to save the books' information")]
    public class Version0000002 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Publishers")
                .WithColumn("Name").AsString().NotNullable().Unique();

            CreateTable("Books")
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("ISBN").AsString().NotNullable()
                .WithColumn("YearPublished").AsString().NotNullable()
                .WithColumn("QuantityAvailable").AsInt32().NotNullable()
                .WithColumn("PublisherId").AsInt64().NotNullable().ForeignKey("FK_Books_Publisher_Id", "Publishers", "Id")
                .OnDelete(System.Data.Rule.None);

            CreateTable("Authors")
                .WithColumn("Name").AsString().NotNullable();

            CreateTable("BooksAuthors")
                .WithColumn("BookId").AsInt64().NotNullable().ForeignKey("FK_BooksAuthors_Book_Id", "Books", "Id")
                .WithColumn("AuthorId").AsInt64().NotNullable().ForeignKey("FK_BooksAuthors_Author_Id", "Authors", "Id")
                .OnDelete(System.Data.Rule.Cascade);
        }
    }
}
