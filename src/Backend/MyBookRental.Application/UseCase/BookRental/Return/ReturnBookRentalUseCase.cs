using MyBookRental.Domain.Repositories;
using MyBookRental.Domain.Repositories.BookRental;
using MyBookRental.Excepetion.ExceptionsBase;

namespace MyBookRental.Application.UseCase.BookRental.Return
{
    public class ReturnBookRentalUseCase : IReturnBookRentalUseCase
    {
        private readonly IBookRentalReadOnlyRepository _readOnlyRepository;
        private readonly IBookRentalWriteOnlyRepository _writeOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReturnBookRentalUseCase(
            IBookRentalReadOnlyRepository readOnlyRepository,
            IBookRentalWriteOnlyRepository writeOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long rentalId)
        {
            var rental = await _readOnlyRepository.GetRentalById(rentalId);
            if (rental == null)
            {
                throw new ErrorOnValidationException(new List<string> { "Locação não encontrada." });
            }

            rental.ActualReturnDate = DateTime.UtcNow;
            rental.Status = "Devolvido";

            await _writeOnlyRepository.Update(rental);
            await _unitOfWork.Commit();
        }
    }
}
