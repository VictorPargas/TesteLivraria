using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.UseCase.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
    }
}
