using Microsoft.Extensions.Configuration;
using MyBookRental.Domain.Enums;

namespace MyBookRental.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static DataBaseType DatabaseType(this IConfiguration configuration)
        {
            var databaseType = configuration.GetConnectionString("DatabaseType");

            return (DataBaseType)Enum.Parse(typeof(DataBaseType), databaseType!);
        }

        public static string ConnectionString(this IConfiguration configuration)
        {
            var databaseType = configuration.DatabaseType();

            if (databaseType == Domain.Enums.DataBaseType.MySql)
                return configuration.GetConnectionString("ConnectionMySQLServer")!;
            else
               return configuration.GetConnectionString("ConnectionSqlServer")!;
        }
    }
}
