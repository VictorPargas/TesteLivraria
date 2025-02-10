using MyBookRental.Communication.Requests;
using MyBookRental.Domain.Repositories;
using MyBookRental.Domain.Repositories.User;
using MyBookRental.Domain.Services.LoggedUser;
using MyBookRental.Exceptions;
using MyBookRental.Exceptions.ExceptionsBase;

namespace MyBookRental.Application.UseCase.User.Update
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly ILooggedUser _looggedUser;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserUseCase(ILooggedUser looggedUser, IUserUpdateOnlyRepository repository, IUserReadOnlyRepository userReadOnlyRepository, IUnitOfWork unitOfWork)
        {
            _looggedUser = looggedUser;
            _repository = repository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestUpdateUserJson request)
        {
            var loggedUser = await _looggedUser.User();

            await Validate(request, loggedUser.Email);

            var user = await _repository.GetById(loggedUser.Id);

            user.Name = request.Name;
            user.Email = request.Email;
            user.Profile = request.Profile;

            _repository.Update(user);

            await _unitOfWork.Commit();
        }

        private async Task Validate(RequestUpdateUserJson request, string currentEmail)
        {
            var validator = new UpdateUserValidator();

            var result = validator.Validate(request);

            if (currentEmail.Equals(request.Email) == false)
            {
                var userExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
                if (userExist)
                    result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessage.EMAIL_ALREADY_REGISTRED));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
