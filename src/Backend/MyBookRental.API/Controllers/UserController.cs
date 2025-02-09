using Microsoft.AspNetCore.Mvc;
using MyBookRental.API.Attributes;
using MyBookRental.Application.UseCase.User.Profile;
using MyBookRental.Application.UseCase.User.Register;
using MyBookRental.Application.UseCase.User.Update;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;

namespace MyBookRental.API.Controllers
{

    public class UserController : MyBookRentalController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices]IRegisterUseCase useCase,
            [FromBody]RequestRegisterUserJson request)
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
        [AuthenticatedUser]
        public async Task<IActionResult> GetUserProfile([FromServices] IGetUserProfileUseCase useCase)
        {
            var result = await useCase.Execute();

            return Ok(result);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateUserUseCase useCase,
            [FromBody] RequestUpdateUserJson request)
        {
            await useCase.Execute(request);
            return NoContent();
        }

        [HttpPut("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser]
        public async Task<IActionResult> ChangePassword(
            [FromServices] IChangePasswordUseCase useCase,
            [FromBody] RequestChangePasswordJson request)
        {
            await useCase.Execute(request);
            return NoContent();
        }
    }
}
