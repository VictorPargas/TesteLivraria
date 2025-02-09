using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.Author;
using MyBookRental.Domain.Repositories;

namespace MyBookRental.Application.UseCase.Author
{
    public class RegisterAuthorUseCase : IRegisterAuthorUseCase
    {
        private readonly IAuthorWriteOnlyRepository _writeOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterAuthorUseCase(
            IAuthorWriteOnlyRepository writeOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredAuthorJson> Execute(RequestAuthorJson request)
        {
            var author = _mapper.Map<Domain.Entities.Author>(request);
            await _writeOnlyRepository.Add(author);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegisteredAuthorJson>(author);
        }
    }
}
