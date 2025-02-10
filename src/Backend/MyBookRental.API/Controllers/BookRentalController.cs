using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBookRental.API.Attributes;
using MyBookRental.Application.UseCase.BookRental.List;
using MyBookRental.Application.UseCase.BookRental.Register;
using MyBookRental.Application.UseCase.BookRental.Renew;
using MyBookRental.Application.UseCase.BookRental.Return;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;

namespace MyBookRental.API.Controllers
{
    [AuthenticatedUser]
    public class BookRentalController : MyBookRentalController
    {
        // Registrar uma nova locação de livro
        [HttpPost("register")]
        [ProducesResponseType(typeof(ResponseRegisteredBookRentalJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterRental(
            [FromServices] IRegisterBookRentalUseCase useCase,
            [FromBody] RequestRegisterBookRentalJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        // Renovar uma locação existente
        [HttpPut("renew/{rentalId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RenewRental(
            [FromServices] IRenewBookRentalUseCase useCase,
            [FromRoute] long rentalId,
            [FromBody] RequestRenewBookRentalJson request)
        {
            await useCase.Execute(rentalId, request);
            return NoContent();
        }

        [HttpGet("my-rentals")]
        [ProducesResponseType(typeof(IEnumerable<ResponseRegisteredBookRentalJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMyRentals(
            [FromServices] IListBookRentalsUseCase useCase)
        {
                var rentals = await useCase.ExecuteForCurrentUser();

                if (rentals == null || !rentals.Any())
                {
                    return Ok(new List<ResponseRegisteredBookRentalJson>());
                }

                return Ok(rentals);
        }

        // Devolver um livro alugado
        [HttpPut("return/{rentalId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReturnRental(
            [FromServices] IReturnBookRentalUseCase useCase,
            [FromRoute] int rentalId)
        {
            await useCase.Execute(rentalId);
            return NoContent();
        }


        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<ResponseRegisteredBookRentalJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
        [AdminOnly] 
        public async Task<IActionResult> GetAllRentals(
            [FromServices] IListBookRentalsUseCase useCase)
        {
            var rentals = await useCase.Execute();

            if (rentals == null || !rentals.Any())
            {
                return Ok(new List<ResponseRegisteredBookRentalJson>());
            }

            return Ok(rentals);
        }
    }
}
