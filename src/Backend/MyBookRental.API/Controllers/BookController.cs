using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBookRental.Application.UseCase.Book.Register;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.Book;

namespace MyBookRental.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredBookJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterBookUseCase useCase,
            [FromBody] RequestRegisterBookJson request)
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ResponseRegisteredBookJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBooks(
            [FromServices] IBookReadOnlyRepository readOnlyRepository)
        {
            var books = await readOnlyRepository.GetAllBooks();
            return Ok(books);
        }
    }
}
