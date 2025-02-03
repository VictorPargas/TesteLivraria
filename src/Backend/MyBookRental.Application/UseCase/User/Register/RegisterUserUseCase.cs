using AutoMapper;
using MyBookRental.Application.Services.AutoMapper;
using MyBookRental.Application.Services.Cryptografy;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories;
using MyBookRental.Domain.Repositories.User;
using MyBookRental.Excepetion;
using MyBookRental.Excepetion.ExceptionsBase;

namespace MyBookRental.Application.UseCase.User.Register
{
    public class RegisterUserUseCase : IRegisterUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PasswordEncripter _passwordEncripter;

        public RegisterUserUseCase(
            IUserWriteOnlyRepository writeOnlyRepository, 
            IUserReadOnlyRepository readOnlyRepository,
            IUnitOfWork unitOfWork,
            PasswordEncripter passwordEncripter,
            IMapper mapper)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _passwordEncripter = passwordEncripter;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = _mapper.Map<Domain.Entities.User>(request);
            user.Password = _passwordEncripter.Encrypt(request.Password);

            await _writeOnlyRepository.Add(user);

            await _unitOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = request.Name
            };
        }

        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (emailExist)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessage.EMAIL_ALREADY_REGISTRED));

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
