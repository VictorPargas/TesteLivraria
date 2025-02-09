using MyBookRental.Domain.Repositories;
using MyBookRental.Domain.Repositories.User;

namespace MyBookRental.Application.UseCase.User.Delete
{
    public class DeleteUserAccountUseCase
    {

        private readonly IUserDeleteOnlyRepository _deleteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        


        public DeleteUserAccountUseCase(
            IUserDeleteOnlyRepository deleteOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _deleteOnlyRepository = deleteOnlyRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
