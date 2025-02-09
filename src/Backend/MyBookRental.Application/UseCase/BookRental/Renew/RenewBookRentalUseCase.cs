using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyBookRental.Domain.Repositories.BookRental;
using MyBookRental.Domain.Repositories;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Excepetion.ExceptionsBase;

namespace MyBookRental.Application.UseCase.BookRental.Renew
{
    public class RenewBookRentalUseCase : IRenewBookRentalUseCase
    {
        private readonly IBookRentalReadOnlyRepository _readOnlyRepository;
        private readonly IBookRentalWriteOnlyRepository _writeOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private const int MaxRenewals = 2;

        public RenewBookRentalUseCase(
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

        public async Task<ResponseRenewedBookRentalJson> Execute(RequestRenewBookRentalJson request)
        {
            var rental = await _readOnlyRepository.GetRentalById(request.RentalId);

            if (rental == null)
                throw new ErrorOnValidationException(new List<string> { "Empréstimo não encontrado." });

            if (rental.Status != "Pendente")
                throw new ErrorOnValidationException(new List<string> { "Apenas empréstimos pendentes podem ser renovados." });

            if (rental.ReturnDate != null)
                throw new ErrorOnValidationException(new List<string> { "Este empréstimo já foi devolvido e não pode ser renovado." });

            if (rental.RenewalsCount >= MaxRenewals)
                throw new ErrorOnValidationException(new List<string> { $"O empréstimo já atingiu o número máximo de {MaxRenewals} renovações." });

            if (request.NewDueDate <= rental.DueDate)
                throw new ErrorOnValidationException(new List<string> { "A nova data de devolução deve ser posterior à data atual de devolução." });

            // Atualiza a data de devolução e o contador de renovações
            rental.DueDate = request.NewDueDate;
            rental.RenewalsCount += 1;

            await _writeOnlyRepository.Update(rental);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRenewedBookRentalJson>(rental);
        }
    }
}
