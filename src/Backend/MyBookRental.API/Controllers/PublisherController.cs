using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBookRental.API.Attributes;
using MyBookRental.Application.UseCase.Publisher;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.Publisher;

namespace MyBookRental.API.Controllers
{

    [AuthenticatedUser]
    public class PublisherController : MyBookRentalController
    {
        [HttpPost("api/publishers")]
        [ProducesResponseType(typeof(ResponseRegisteredPublisherJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterPublisherUseCase useCase,
            [FromBody] RequestPublisherJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpGet("api/publishers")]
        [ProducesResponseType(typeof(IEnumerable<ResponseRegisteredPublisherJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPublishers(
            [FromServices] IPublisherReadOnlyRepository readOnlyRepository)
        {
            var publishers = await readOnlyRepository.GetAllPublishers();
            return Ok(publishers);
        }
    }
}
