using AutoMapper;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.BookRental;
using MyBookRental.Domain.Repositories.User;
using MyBookRental.Domain.Services.LoggedUser;

namespace MyBookRental.Application.UseCase.BookRental.List
{
    public class ListBookRentalsUseCase : IListBookRentalsUseCase
    {

        private readonly IBookRentalReadOnlyRepository _readOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly ILooggedUser _looggedUser;

        public ListBookRentalsUseCase(IBookRentalReadOnlyRepository readOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository, IMapper mapper, ILooggedUser looggedUser)
        {
            _readOnlyRepository = readOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
            _looggedUser = looggedUser;
        }

        public async Task<IEnumerable<ResponseRegisteredBookRentalJson>> Execute()
        {

            var rentals = await _readOnlyRepository.GetAllRentals();

            return _mapper.Map<IEnumerable<ResponseRegisteredBookRentalJson>>(rentals);
        }

        public async Task<IEnumerable<ResponseRegisteredBookRentalJson>> ExecuteForCurrentUser()
        {
            var loggedUser = await _looggedUser.User();

            var rentals = await _readOnlyRepository.GetRentalsByUserIdentifier(loggedUser.UserIdentifier);

            return _mapper.Map<IEnumerable<ResponseRegisteredBookRentalJson>>(rentals);
        }
    }
}
