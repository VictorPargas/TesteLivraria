using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBookRental.Application.Services.AutoMapper;
using MyBookRental.Application.UseCase.Author;
using MyBookRental.Application.UseCase.Book.Delete;
using MyBookRental.Application.UseCase.Book.Get;
using MyBookRental.Application.UseCase.Book.Register;
using MyBookRental.Application.UseCase.Book.Search;
using MyBookRental.Application.UseCase.Book.Update;
using MyBookRental.Application.UseCase.BookRental.List;
using MyBookRental.Application.UseCase.BookRental.Register;
using MyBookRental.Application.UseCase.BookRental.Renew;
using MyBookRental.Application.UseCase.BookRental.Return;
using MyBookRental.Application.UseCase.Login.DoLogin;
using MyBookRental.Application.UseCase.Publisher;
using MyBookRental.Application.UseCase.User.Delete;
using MyBookRental.Application.UseCase.User.List;
using MyBookRental.Application.UseCase.User.Profile;
using MyBookRental.Application.UseCase.User.Register;
using MyBookRental.Application.UseCase.User.Update;

namespace MyBookRental.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IListUsersUseCase, ListUsersUseCase>();
            services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();


            services.AddScoped<IRegisterBookUseCase, RegisterBookUseCase>();
            services.AddScoped<IRegisterAuthorUseCase, RegisterAuthorUseCase>();
            services.AddScoped<IRegisterPublisherUseCase, RegisterPublisherUseCase>();
            services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
            services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
            services.AddScoped<IGetBooksUseCase, GetBooksUseCase>();
            services.AddScoped<ISearchBooksUseCase, SearchBooksUseCase>();

            services.AddScoped<IRegisterBookRentalUseCase, RegisterBookRentalUseCase>();
            services.AddScoped<IRenewBookRentalUseCase, RenewBookRentalUseCase>();
            services.AddScoped<IReturnBookRentalUseCase, ReturnBookRentalUseCase>();
            services.AddScoped<IListBookRentalsUseCase, ListBookRentalsUseCase>();
        }
    }
}
