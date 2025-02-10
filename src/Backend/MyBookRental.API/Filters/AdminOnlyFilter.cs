using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.User;
using MyBookRental.Domain.Security.Tokens;
using MyBookRental.Exceptions;
using MyBookRental.Excepetion.ExceptionsBase;

namespace MyBookRental.API.Filters
{
    public class AdminOnlyFilter : IAsyncAuthorizationFilter
    {
        private readonly IAccessTokenValidator _accessTokenValidator;
        private readonly IUserReadOnlyRepository _repository;

        public AdminOnlyFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _accessTokenValidator = accessTokenValidator;
            _repository = userReadOnlyRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context);

                // Valida o token e obtém o identificador do usuário
                var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

                // Verifica se o usuário existe e está ativo
                var user = await _repository.GetUserByIdentifierAsync(userIdentifier);
                if (user == null || !user.Active)
                {
                    throw new MyBookRentalException(ResourceMessage.NO_TOKEN);
                }

                // Verifica se o usuário é administrador
                if (user.Profile != "Administrador")
                {
                    context.Result = new ForbidResult(); // Retorna 403 Forbidden
                    return;
                }
            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("TokenIsExpired")
                {
                    TokenIsExpired = true,
                });
            }
            catch (MyBookRentalException ex)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessage.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
            }
        }

        private static string TokenOnRequest(AuthorizationFilterContext context)
        {
            var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
            if (string.IsNullOrWhiteSpace(authentication))
            {
                throw new MyBookRentalException(ResourceMessage.NO_TOKEN);
            }

            return authentication["Bearer ".Length..].Trim();
        }
    }
}
