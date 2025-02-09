using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.Book;

namespace MyBookRental.Application.UseCase.Book.Get
{
    public class GetBooksUseCase : IGetBooksUseCase
    {
        private readonly IBookReadOnlyRepository _readOnlyRepository;
        private readonly IMapper _mapper;

        public GetBooksUseCase(IBookReadOnlyRepository readOnlyRepository, IMapper mapper)
        {
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
        }

        public async Task<IList<ResponseRegisteredBookJson>> Execute()
        {
            var books = await _readOnlyRepository.GetAllBooksWithDetails();
            return _mapper.Map<IList<ResponseRegisteredBookJson>>(books);
        }

    }
}
