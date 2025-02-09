using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookRental.Domain.Entities;

namespace MyBookRental.Domain.Services.LoggedUser
{
    public interface ILooggedUser
    {
        public Task<User> User();
    }
}
