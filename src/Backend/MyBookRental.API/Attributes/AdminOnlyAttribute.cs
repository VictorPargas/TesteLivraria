using Microsoft.AspNetCore.Mvc;
using MyBookRental.API.Filters;

namespace MyBookRental.API.Attributes
{
    public class AdminOnlyAttribute : TypeFilterAttribute
    {
        public AdminOnlyAttribute() : base(typeof(AdminOnlyFilter))
        {
        }
    }
}
