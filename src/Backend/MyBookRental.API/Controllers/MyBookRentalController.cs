using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MyBookRental.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MyBookRentalController : ControllerBase
    {

        protected static bool IsNotAuthenticated(AuthenticateResult authenticate)
        {
            return authenticate.Succeeded == false
                || authenticate.Principal is null
                || authenticate.Principal.Identities.Any(id => id.IsAuthenticated) == false;
        }
    }
}
