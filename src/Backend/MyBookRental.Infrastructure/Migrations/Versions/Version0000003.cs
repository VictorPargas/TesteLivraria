using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace MyBookRental.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_BOOKSRENTAL, "Create table to save the booksRental' information")]
    public class Version0000003 : VersionBase
    {
        public override void Up()
        {
            CreateTable("BooksRental")
               .WithColumn("RentalDate").AsDateTime().NotNullable()
               .WithColumn("ExpectedReturnDate").AsDateTime().NotNullable()
               .WithColumn("ActualReturnDate").AsDateTime().Nullable()
               .WithColumn("Fine").AsDecimal().Nullable()
               .WithColumn("Status").AsString(50).NotNullable().WithDefaultValue("Pendente")
               .WithColumn("BookId").AsInt64().NotNullable().ForeignKey("FK_BooksRental_Book_Id", "Books", "Id")
               .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_BooksRental_User_Id", "Users", "Id");

            CreateTable("BooksRentalDetails")
               .WithColumn("RentalId").AsInt64().NotNullable().ForeignKey("FK_BooksRentalDetails_Rental_Id", "BooksRental", "Id").OnDelete(System.Data.Rule.Cascade)
               .WithColumn("BookId").AsInt64().NotNullable().ForeignKey("FK_BooksRentalDetails_Book_Id", "Books", "Id").OnDelete(System.Data.Rule.Cascade)
               .WithColumn("Quantity").AsInt32().NotNullable();
        }
    }
}
