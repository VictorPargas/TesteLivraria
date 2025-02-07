using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyBookRental.Application.UseCase.Book.Register;
using MyBookRental.Domain.Repositories.BookRental;
using MyBookRental.Domain.Repositories;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Excepetion.ExceptionsBase;
using MyBookRental.Domain.Entities;

namespace MyBookRental.Application.UseCase.BookRental.Register
{
    public class RegisterBookRentalUseCase : IRegisterBookRentalUseCase
    {
        private readonly IBookRentalWriteOnlyRepository _writeOnlyRepository;
        private readonly IBookRentalReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterBookRentalUseCase(
            IBookRentalWriteOnlyRepository writeOnlyRepository,
            IBookRentalReadOnlyRepository readOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredBookRentalJson> Execute(RequestRegisterBookRentalJson request)
        {
            var isAvailable = await _readOnlyRepository.IsBookAvailable(request.BookId);
            if (!isAvailable)
            {
                throw new ErrorOnValidationException(new List<string> { "Livro indisponível para locação." });
            }

            var rental = new Domain.Entities.BookRental
            {
                UserId = request.UserId,
                BookId = request.BookId,
                RentalDate = DateTime.UtcNow,
                DueDate = request.DueDate,
                Status = "Pendente"
            };

            await _writeOnlyRepository.Add(rental);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegisteredBookRentalJson>(rental);
        }
    }
}
