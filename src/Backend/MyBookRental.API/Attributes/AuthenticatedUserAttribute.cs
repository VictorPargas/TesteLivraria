using Microsoft.AspNetCore.Mvc;
using MyBookRental.API.Filters;

namespace MyBookRental.API.Attributes
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
        {
        }
    }
}
