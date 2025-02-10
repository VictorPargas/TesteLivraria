using MyBookRental.Infrastructure.Migrations.Versions;
using MyBookRental.Infrastructure.Migrations;
using FluentMigrator;

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
           .WithColumn("BookId").AsInt64().NotNullable()
               .ForeignKey("FK_BooksRental_Book_Id", "Books", "Id")
               .OnDelete(System.Data.Rule.None)
           .WithColumn("UserId").AsInt64().NotNullable()
               .ForeignKey("FK_BooksRental_User_Id", "Users", "Id")
               .OnDelete(System.Data.Rule.None);

        CreateTable("BooksRentalDetails")
           .WithColumn("RentalId").AsInt64().NotNullable()
               .ForeignKey("FK_BooksRentalDetails_Rental_Id", "BooksRental", "Id")
               .OnDelete(System.Data.Rule.Cascade)
           .WithColumn("BookId").AsInt64().NotNullable()
               .ForeignKey("FK_BooksRentalDetails_Book_Id", "Books", "Id")
               .OnDelete(System.Data.Rule.None) 
           .WithColumn("Quantity").AsInt32().NotNullable();
    }
}
