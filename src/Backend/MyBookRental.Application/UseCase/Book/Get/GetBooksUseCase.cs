using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.Book;
using MyBookRental.Domain.Repositories.BookRental;

namespace MyBookRental.Application.UseCase.Book.Get
{
    public class GetBooksUseCase : IGetBooksUseCase
    {
        private readonly IBookReadOnlyRepository _readOnlyRepository;
        IBookRentalReadOnlyRepository _bookRentalRepository;
        private readonly IMapper _mapper;

        public GetBooksUseCase(IBookReadOnlyRepository readOnlyRepository, IBookRentalReadOnlyRepository bookRentalRepository, IMapper mapper)
        {
            _readOnlyRepository = readOnlyRepository;
            _bookRentalRepository = bookRentalRepository;
            _mapper = mapper;
        }

        public async Task<IList<ResponseRegisteredBookJson>> Execute()
        {

            var books = await _readOnlyRepository.GetAllBooksWithDetails();

            // Mapeia os livros para o DTO de resposta
            var response = _mapper.Map<IList<ResponseRegisteredBookJson>>(books);

            // Para cada livro, verifica se está disponível e atualiza o DTO
            foreach (var book in response)
            {
                book.IsAvailable = await _bookRentalRepository.IsBookAvailable(book.Id);
            }

            return response;
        }

    }
}
