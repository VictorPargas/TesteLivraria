using MyBookRental.Application.Services.AutoMapper;
using MyBookRental.Application.Services.Cryptografy;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Excepetion.ExceptionsBase;

namespace MyBookRental.Application.UseCase.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
        {
            var criptografiaDeSenha = new PasswordEncripter();
            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();

            Validate(request);

            var user = autoMapper.Map<Domain.Entities.User>(request);

            user.Password = criptografiaDeSenha.Encrypt(request.Password);

            return new ResponseRegisteredUserJson
            {
                Name = request.Name
            };
        }

        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
