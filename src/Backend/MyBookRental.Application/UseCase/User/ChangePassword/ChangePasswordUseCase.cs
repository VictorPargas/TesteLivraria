using MyBookRental.Communication.Requests;
using MyBookRental.Domain.Repositories;
using MyBookRental.Domain.Repositories.User;
using MyBookRental.Domain.Security.Cryptography;
using MyBookRental.Domain.Services.LoggedUser;
using MyBookRental.Exceptions;
using MyBookRental.Exceptions.ExceptionsBase;

namespace MyBookRental.Application.UseCase.User.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly ILooggedUser _looggedUser;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordEncripter _passwordEncripter;

        public ChangePasswordUseCase(ILooggedUser looggedUser, IUserUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IPasswordEncripter passwordEncripter)
        {
            _looggedUser = looggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _passwordEncripter = passwordEncripter;
        }

        public async Task Execute(RequestChangePasswordJson request)
        {
            var loggedUser = await _looggedUser.User();

            Validate(request, loggedUser);
            
            var user = await _repository.GetById(loggedUser.Id);

            user.Password = _passwordEncripter.Encrypt(request.NewPassword);

            _repository.Update(user);

            await _unitOfWork.Commit();
        }

        private void Validate(RequestChangePasswordJson request, Domain.Entities.User loggedUser)
        {
            var result = new ChangePasswordValidator().Validate(request);

            var currentePassswordEncripted = _passwordEncripter.Encrypt(request.Password);

            if (currentePassswordEncripted.Equals(loggedUser.Password) == false)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessage.PASSWORD_EMPTY)); // Comparação de criptrfai 

            if (result.IsValid == false)
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}
