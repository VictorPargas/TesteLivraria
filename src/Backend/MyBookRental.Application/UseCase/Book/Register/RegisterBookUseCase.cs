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

namespace MyBookRental.Application.UseCase.Book.Register
{
    public class RegisterBookUseCase : IRegisterBookUseCase
    {
        private readonly IBookWriteOnlyRepository _writeOnlyRepository;
        private readonly IBookReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterBookUseCase(
           IBookWriteOnlyRepository writeOnlyRepository,
           IBookReadOnlyRepository readOnlyRepository,
           IUnitOfWork unitOfWork,
           IMapper mapper)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ResponseRegisteredBookJson> Execute(RequestRegisterBookJson request)
        {
            await Validate(request);

            var book = _mapper.Map<Domain.Entities.Book>(request);

            await _writeOnlyRepository.Add(book);
            await _unitOfWork.Commit();

            return new ResponseRegisteredBookJson
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            };
        }

        private async Task Validate(RequestRegisterBookJson request)
        {
            var validator = new RegisterBookValidator();
            var result = validator.Validate(request);

            var isbnExists = await _readOnlyRepository.ExistsBookWithISBN(request.ISBN);
            if (isbnExists)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.ISBN), "ISBN já cadastrado!"));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
