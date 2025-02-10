using AutoMapper;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.Book;
using MyBookRental.Domain.Repositories;
using MyBookRental.Infrastructure.DataAccess.Repositories;
using MyBookRental.Exceptions.ExceptionsBase;
using FluentValidation;
using MyBookRental.Domain.Entities;

namespace MyBookRental.Application.UseCase.Book.Update
{
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        private readonly IBookWriteOnlyRepository _writeOnlyRepository;
        private readonly IBookReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBookUseCase(
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


        private static void Validate(RequestUpdateBookJson request)
        {
            var result = new UpdateBookValidator().Validate(request);

            if (!result.IsValid)
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }

        public async Task<ResponseRegisteredBookJson> Execute(int id, RequestUpdateBookJson request)
        {
            Validate(request);

            var book = await _readOnlyRepository.GetBookWithDetails(id);
            if (book == null)
                throw new ErrorOnValidationException(new List<string> { "Livro não encontrado." });

            // Atualiza apenas os campos fornecidos
            if (request.Title != null) book.Title = request.Title;
            if (request.ISBN != null) book.ISBN = request.ISBN;
            if (request.Quantity.HasValue) book.QuantityAvailable = request.Quantity.Value;
            if (request.Year.HasValue) book.YearPublished = request.Year.ToString();
            if (request.Publisher != null) book.Publisher.Name = request.Publisher;

            // Atualização dos autores (se fornecido)
            if (request.AuthorIds != null && request.AuthorIds.Any())
            {
                // Remove autores antigos
                book.BookAuthors.Clear();

                // Adiciona novos autores
                foreach (var authorId in request.AuthorIds)
                {
                    book.BookAuthors.Add(new BookAuthor
                    {
                        BookId = book.Id,
                        AuthorId = authorId
                    });
                }
            }

            await _writeOnlyRepository.Update(book);
            await _unitOfWork.Commit();

            var updatedBook = await _readOnlyRepository.GetBookWithDetails(book.Id);

            return _mapper.Map<ResponseRegisteredBookJson>(updatedBook);
        }

    }
}
