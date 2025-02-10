using MyBookRental.Communication.Requests;
using MyBookRental.Domain.Repositories;
using MyBookRental.Domain.Repositories.BookRental;
using MyBookRental.Exceptions.ExceptionsBase;

namespace MyBookRental.Application.UseCase.BookRental.Renew
{

    public class RenewBookRentalUseCase : IRenewBookRentalUseCase
    {
        private readonly IBookRentalReadOnlyRepository _readOnlyRepository;
        private readonly IBookRentalWriteOnlyRepository _writeOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RenewBookRentalUseCase(
           IBookRentalReadOnlyRepository readOnlyRepository,
           IBookRentalWriteOnlyRepository writeOnlyRepository,
           IUnitOfWork unitOfWork)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long rentalId, RequestRenewBookRentalJson request)
        {
            var rental = await _readOnlyRepository.GetRentalById(rentalId);
            if (rental == null)
            {
                throw new ErrorOnValidationException(new List<string> { "Locação não encontrada." });
            }

            if (request.NewDueDate <= DateTime.UtcNow)
            {
                throw new ErrorOnValidationException(new List<string> { "A nova data de devolução deve ser no futuro." });
            }

            rental.ExpectedReturnDate = request.NewDueDate;
            rental.Status = "Renovado";

            await _writeOnlyRepository.Update(rental);
            await _unitOfWork.Commit();
        }
    }
}
