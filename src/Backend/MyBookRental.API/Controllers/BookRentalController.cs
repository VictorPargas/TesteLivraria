using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBookRental.Application.UseCase.BookRental.Register;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.BookRental;

namespace MyBookRental.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookRentalController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredBookRentalJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
           [FromServices] IRegisterBookRentalUseCase useCase,
           [FromBody] RequestRegisterBookRentalJson request)
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ResponseBookRentalDetailsJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllRentals(
            [FromServices] IBookRentalReadOnlyRepository readOnlyRepository,
            [FromServices] IMapper mapper,
            [FromQuery] string? status = null,
            [FromQuery] long? userId = null)
        {
            var rentals = await readOnlyRepository.GetAllRentals();

            if (!rentals.Any())
            {
                return NotFound("Nenhuma locação encontrada no sistema.");
            }

            if (!string.IsNullOrEmpty(status))
            {
                rentals = rentals.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            if (userId.HasValue)
            {
                rentals = rentals.Where(r => r.UserId == userId.Value);
            }
            var response = mapper.Map<IEnumerable<ResponseBookRentalDetailsJson>>(rentals);
            return Ok(response);
        }
    }
}
