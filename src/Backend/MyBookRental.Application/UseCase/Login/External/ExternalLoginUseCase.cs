using MyBookRental.Domain.Repositories;
using MyBookRental.Domain.Repositories.User;

namespace MyBookRental.Application.UseCase.Login.External
{
    public class ExternalLoginUseCase : IExternalLoginUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;


        public ExternalLoginUseCase(
            IUserReadOnlyRepository userReadOnlyRepository,
            IUserWriteOnlyRepository userWriteOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Execute(string name, string email)
        {
            var user = await _userReadOnlyRepository.GetByEmail(email);

            if (user is null)
            {
                user = new Domain.Entities.User
                {
                    Name = name,
                    Email = email,
                    Password = "_"
                };

                await _userWriteOnlyRepository.Add(user);
                await _unitOfWork.Commit();
            }

            
           return "Usuário não encontrado";

        }
    }
}
