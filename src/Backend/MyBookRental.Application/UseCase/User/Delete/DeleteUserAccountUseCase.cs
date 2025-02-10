using MyBookRental.Domain.Repositories;
using MyBookRental.Domain.Repositories.BookRental;
using MyBookRental.Domain.Repositories.User;
using MyBookRental.Domain.Services.LoggedUser;
using MyBookRental.Exceptions;
using MyBookRental.Exceptions.ExceptionsBase;

namespace MyBookRental.Application.UseCase.User.Delete
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUserDeleteOnlyRepository _deleteOnlyRepository;
        private readonly IBookRentalReadOnlyRepository _bookRentalRepository;
        private readonly ILooggedUser _looggedUser;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteUserUseCase(
             IUserReadOnlyRepository readOnlyRepository,
             IUserDeleteOnlyRepository deleteOnlyRepository,
             IBookRentalReadOnlyRepository bookRentalRepository,
             ILooggedUser looggedUser,
             IUnitOfWork unitOfWork)
        {
            _readOnlyRepository = readOnlyRepository;
            _deleteOnlyRepository = deleteOnlyRepository;
            _bookRentalRepository = bookRentalRepository;
            _looggedUser = looggedUser;
            _unitOfWork = unitOfWork;
        }


        public async Task Execute()
        {
            var user = await _looggedUser.User();
            await DeleteUser(user.UserIdentifier, isAdmin: false);
        }

        public async Task Execute(Guid userIdentifier)
        {
            await DeleteUser(userIdentifier, isAdmin: true);
        }

        private async Task DeleteUser(Guid userIdentifier, bool isAdmin)
        {
            var user = await _readOnlyRepository.GetUserByIdentifierAsync(userIdentifier);

            if (user == null)
                throw new MyBookRentalException("Usuário não encontrado.");

            var hasRentals = await _bookRentalRepository.HasRentals(userIdentifier);
            if (hasRentals)
                throw new MyBookRentalException("Não é possível excluir o usuário com locações associadas.");

            await _deleteOnlyRepository.DeleteAccount(userIdentifier);
            await _unitOfWork.Commit();
        }
    }
}
