using Microsoft.AspNetCore.Mvc;
using MyBookRental.API.Attributes;
using MyBookRental.Application.UseCase.Book.Get;
using MyBookRental.Application.UseCase.Book.Register;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;

namespace MyBookRental.API.Controllers
{

    [AuthenticatedUser]
    public class BookController : MyBookRentalController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredBookJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterBookUseCase useCase,
            [FromBody] RequestBookJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }


        [HttpGet]
        [ProducesResponseType(typeof(IList<ResponseRegisteredBookJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBooks(
            [FromServices] IGetBooksUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }
    }
}
