using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBookRental.API.Attributes;
using MyBookRental.Application.UseCase.Author;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.Author;

namespace MyBookRental.API.Controllers
{
    [AuthenticatedUser]
    public class AuthorController : MyBookRentalController
    {
        [HttpPost("api/authors")]
        [ProducesResponseType(typeof(ResponseRegisteredAuthorJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterAuthorUseCase useCase,
            [FromBody] RequestAuthorJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpGet("api/authors")]
        [ProducesResponseType(typeof(IEnumerable<ResponseRegisteredAuthorJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAuthors(
            [FromServices] IAuthorReadOnlyRepository readOnlyRepository)
        {
            var authors = await readOnlyRepository.GetAllAuthors();
            return Ok(authors);
        }

    }
}
