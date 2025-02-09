using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyBookRental.Domain.Repositories.Book;
using MyBookRental.Domain.Repositories;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Excepetion.ExceptionsBase;
using MyBookRental.Domain.Services.LoggedUser;
using FluentValidation;

namespace MyBookRental.Application.UseCase.Book.Register
{
    public class RegisterBookUseCase : IRegisterBookUseCase
    {
        private readonly IBookWriteOnlyRepository _writeOnlyRepository;
        private readonly ILooggedUser _looggedUser;
        private readonly IBookReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterBookUseCase(
           IBookWriteOnlyRepository writeOnlyRepository,
           ILooggedUser looggedUser,
           IBookReadOnlyRepository readOnlyRepository,
           IUnitOfWork unitOfWork,
           IMapper mapper)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _looggedUser = looggedUser;
            _readOnlyRepository = readOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredBookJson> Execute(RequestBookJson request)
        {
            Validate(request);

            var publisherExists = await _readOnlyRepository.ExistsPublisher(request.PublisherId);
            if (!publisherExists)
                throw new ErrorOnValidationException(new List<string> { "A editora informada não existe." });

            var book = _mapper.Map<Domain.Entities.Book>(request);

            await _writeOnlyRepository.Add(book);
            await _unitOfWork.Commit();

            var bookWithDetails = await _readOnlyRepository.GetBookWithDetails(book.Id);

            return _mapper.Map<ResponseRegisteredBookJson>(book);
        }


        private static void Validate(RequestBookJson request)
        {
            var result = new BookValidator().Validate(request);

            if (result.IsValid == false)
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());

        }
    }
}
