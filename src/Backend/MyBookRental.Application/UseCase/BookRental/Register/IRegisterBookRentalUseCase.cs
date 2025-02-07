using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.UseCase.BookRental.Register
{
    public interface IRegisterBookRentalUseCase
    {
        Task<ResponseRegisteredBookRentalJson> Execute(RequestRegisterBookRentalJson request);
    }
}
