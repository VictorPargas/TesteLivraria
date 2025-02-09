using AutoMapper;
using MyBookRental.Domain.Repositories.BookRental;
using MyBookRental.Domain.Repositories;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Excepetion.ExceptionsBase;

namespace MyBookRental.Application.UseCase.BookRental.Return
{
    public class ReturnBookRentalUseCase : IReturnBookRentalUseCase
    {
        private readonly IBookRentalReadOnlyRepository _readOnlyRepository;
        private readonly IBookRentalWriteOnlyRepository _writeOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private const decimal LateFeePerDay = 5.00m;

        public ReturnBookRentalUseCase(
           IBookRentalReadOnlyRepository readOnlyRepository,
           IBookRentalWriteOnlyRepository writeOnlyRepository,
           IUnitOfWork unitOfWork,
           IMapper mapper)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseReturnedBookRentalJson> Execute(RequestReturnBookRentalJson request)
        {
            var rental = await _readOnlyRepository.GetRentalById(request.RentalId);

            if (rental == null)
                throw new ErrorOnValidationException(new List<string> { "Empréstimo não encontrado." });

            if (rental.Status != "Pendente")
                throw new ErrorOnValidationException(new List<string> { "Este empréstimo já foi devolvido." });

            // Atualizar a data de devolução
            rental.ReturnDate = DateTime.UtcNow;

            // Calcular a multa se houver atraso
            if (rental.ReturnDate > rental.DueDate)
            {
                var daysLate = (rental.ReturnDate.Value.Date - rental.DueDate.Date).Days;
                rental.LateFee = daysLate * LateFeePerDay;
                rental.Status = "Atrasado";
            }
            else
            {
                rental.LateFee = 0;
                rental.Status = "Devolvido";
            }

            await _writeOnlyRepository.Update(rental);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseReturnedBookRentalJson>(rental);
        }
    }
}
