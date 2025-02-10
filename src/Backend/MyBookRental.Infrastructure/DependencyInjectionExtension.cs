using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBookRental.Domain.Enums;
using MyBookRental.Domain.Repositories;
using MyBookRental.Domain.Repositories.Author;
using MyBookRental.Domain.Repositories.Book;
using MyBookRental.Domain.Repositories.BookRental;
using MyBookRental.Domain.Repositories.Publisher;
using MyBookRental.Domain.Repositories.User;
using MyBookRental.Domain.Security.Cryptography;
using MyBookRental.Domain.Security.Tokens;
using MyBookRental.Domain.Services.LoggedUser;
using MyBookRental.Infrastructure.DataAccess;
using MyBookRental.Infrastructure.DataAccess.Repositories;
using MyBookRental.Infrastructure.Extensions;
using MyBookRental.Infrastructure.Security.Cryptography;
using MyBookRental.Infrastructure.Security.Tokens.Access.Generator;
using MyBookRental.Infrastructure.Security.Tokens.Access.Validator;
using MyBookRental.Infrastructure.Services.LoggedUser;

namespace MyBookRental.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddPasswordEncrpited(services, configuration);
            AddRepositories(services);
            AddLoggeduser(services);
            AddTokens(services, configuration);
            
            var databasseType = configuration.DatabaseType();

            if (databasseType == DataBaseType.SqlServer)
            {
                AddDbContext_SqlServer(services, configuration);
                AddFluentMigrator_SqlServer(services, configuration);
            }
            else
            {
                AddDbContext_MySqlServer(services, configuration);
                AddFluentMigrator_MySql(services, configuration);
            }
        }

        private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            services.AddDbContext<MyBookRentalDbContext>(dbContenxtOptions =>
            {
                dbContenxtOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddDbContext_MySqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

            services.AddDbContext<MyBookRentalDbContext>(dbContenxtOptions =>
            {
                dbContenxtOptions.UseMySql(connectionString, serverVersion);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
            services.AddScoped<IUserDeleteOnlyRepository, UserRepository>();


            services.AddScoped<IBookWriteOnlyRepository, BookRepository>();
            services.AddScoped<IBookReadOnlyRepository, BookRepository>();

            // Repositórios de Autores
            services.AddScoped<IAuthorWriteOnlyRepository, AuthorRepository>();
            services.AddScoped<IAuthorReadOnlyRepository, AuthorRepository>();

            // Repositórios de Editoras
            services.AddScoped<IPublisherWriteOnlyRepository, PublisherRepository>();
            services.AddScoped<IPublisherReadOnlyRepository, PublisherRepository>();

            services.AddScoped<IBookRentalReadOnlyRepository, BookRentalRepository>();
            services.AddScoped<IBookRentalWriteOnlyRepository, BookRentalRepository>();




            //services.AddScoped<IBookRentalWriteOnlyRepository, BookRentalRepository>();
            //services.AddScoped<IBookRentalReadOnlyRepository, BookRentalRepository>();

        }

        private static void AddFluentMigrator_MySql(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                .AddMySql5()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("MyBookRental.Infrastructure")).For.All();
            });
        }

        private static void AddFluentMigrator_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("MyBookRental.Infrastructure")).For.All();
            });
        }

        private static void AddTokens(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(options => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
            services.AddScoped<IAccessTokenValidator>(options => new JwtTokenValidator(signingKey!));
        }

        private static void AddLoggeduser(IServiceCollection services) => services.AddScoped<ILooggedUser, LoggedUser>();

        private static void AddPasswordEncrpited(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

            services.AddScoped<IPasswordEncripter>(option => new Sha512Encripter(additionalKey!));
        }
    }

}
