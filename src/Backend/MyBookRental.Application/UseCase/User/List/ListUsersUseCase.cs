using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.User;

namespace MyBookRental.Application.UseCase.User.List
{
    public class ListUsersUseCase : IListUsersUseCase
    {
        private readonly IUserReadOnlyRepository _userRepository;

        public ListUsersUseCase(IUserReadOnlyRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<ResponseUserJson>> Execute()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return users.Select(user => new ResponseUserJson
            {
                Id = user.UserIdentifier,
                Name = user.Name,
                Email = user.Email
            });
        }
    }
}
