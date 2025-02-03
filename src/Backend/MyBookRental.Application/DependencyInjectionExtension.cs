using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBookRental.Application.Services.AutoMapper;
using MyBookRental.Application.Services.Cryptografy;
using MyBookRental.Application.UseCase.User.Register;

namespace MyBookRental.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCases(services);
            AddPasswordEncrpited(services, configuration);
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
        }

        private static void AddPasswordEncrpited(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

            services.AddScoped(option => new PasswordEncripter(additionalKey!));
        }
    }
}
