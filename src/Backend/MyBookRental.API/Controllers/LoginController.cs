using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using MyBookRental.Application.UseCase.Login.DoLogin;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using System.Security.Claims;
namespace MyBookRental.API.Controllers
{
    public class LoginController : MyBookRentalController
    {

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("google")]
        public async Task<IActionResult> LoginGoogle(
            string returnUrl)
        {
          var authenticate =  await Request.HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
          
            if (IsNotAuthenticated(authenticate))
            {
                return Challenge(GoogleDefaults.AuthenticationScheme); //Redireciona para a página de login do Google
            }
            else
            {
                var claims = authenticate.Principal!.Identities.First().Claims;

                var name = claims.First(c => c.Type == ClaimTypes.Name).Value;
                var email = claims.First(c => c.Type == ClaimTypes.Email).Value;

                var token = "fdsfdsf";

                return Redirect($"{returnUrl}{token}");
            }
        }
    }
}
