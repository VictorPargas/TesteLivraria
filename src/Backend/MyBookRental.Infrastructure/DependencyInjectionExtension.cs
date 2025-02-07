using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBookRental.Domain.Enums;
using MyBookRental.Domain.Repositories;
using MyBookRental.Domain.Repositories.Book;
using MyBookRental.Domain.Repositories.BookRental;
using MyBookRental.Domain.Repositories.User;
using MyBookRental.Infrastructure.DataAccess;
using MyBookRental.Infrastructure.DataAccess.Repositories;

namespace MyBookRental.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseType = configuration.GetConnectionString("DatabaseType");

            var databaseTypeEnum = (DataBaseType)Enum.Parse(typeof(DataBaseType), databaseType!);

            if (databaseTypeEnum == DataBaseType.SqlServer)
                AddDbContext_SqlServer(services, configuration);
            else
                AddDbContext_MySqlServer(services, configuration);
      
            AddRepositories(services);
        }

        private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionSqlServer");
            services.AddDbContext<MyBookRentalDbContext>(dbContenxtOptions =>
            {
                dbContenxtOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddDbContext_MySqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionSqlServer");
            services.AddDbContext<MyBookRentalDbContext>(dbContenxtOptions =>
            {
                dbContenxtOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IBookWriteOnlyRepository, BookRepository>();
            services.AddScoped<IBookReadOnlyRepository, BookRepository>();
            services.AddScoped<IBookRentalWriteOnlyRepository, BookRentalRepository>();
            services.AddScoped<IBookRentalReadOnlyRepository, BookRentalRepository>();
        }
    }
}
