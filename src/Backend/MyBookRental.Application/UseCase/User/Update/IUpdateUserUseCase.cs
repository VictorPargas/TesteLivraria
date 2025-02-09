using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookRental.Communication.Requests;

namespace MyBookRental.Application.UseCase.User.Update
{
    public interface IUpdateUserUseCase
    {
        Task Execute(RequestUpdateUserJson request);
    }
}
