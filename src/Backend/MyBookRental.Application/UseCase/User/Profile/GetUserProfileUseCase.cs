using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Services.LoggedUser;

namespace MyBookRental.Application.UseCase.User.Profile
{
    public class GetUserProfileUseCase : IGetUserProfileUseCase
    {
        private readonly ILooggedUser _looggedUser;
        private readonly IMapper _mapper;
        public GetUserProfileUseCase(ILooggedUser looggedUser, IMapper mapper) 
        {
            _looggedUser = looggedUser;
            _mapper = mapper;
        }
        public async Task<ResponseUserProfileJson> Execute()
        {
            var user = await _looggedUser.User();

           return _mapper.Map<ResponseUserProfileJson>(user);
        }
    }
}
