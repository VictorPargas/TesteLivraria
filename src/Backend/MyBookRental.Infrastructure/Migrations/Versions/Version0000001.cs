using FluentMigrator;

namespace MyBookRental.Infrastructure.Migrations.Versions

{
    [Migration(DatabaseVersions.TABLE_USER, "Create table to sabe the user's information")]
    public class Version0000001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Users")
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("Email").AsString(50).NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable()
                .WithColumn("Telefone").AsString(50).NotNullable()
                .WithColumn("Perfil").AsString(50).NotNullable()
                .WithColumn("UserIdentifier").AsGuid().NotNullable();        
        }
    }
}
