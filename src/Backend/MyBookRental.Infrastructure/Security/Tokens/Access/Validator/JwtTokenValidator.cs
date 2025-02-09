using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MyBookRental.Domain.Security.Tokens;

namespace MyBookRental.Infrastructure.Security.Tokens.Access.Validator
{
    //Valida o token se tudo estiver correto e retorna o identificador do usuário
    public class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
    {
        private readonly string _signinKey;

        public JwtTokenValidator(string signinKey) => _signinKey = signinKey;

        public Guid ValidateAndGetUserIdentifier(string token)
        {
            var validationParameter = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = SecurityKey(_signinKey),
                ClockSkew = new TimeSpan(0)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, validationParameter, out _);

            var userIdentifier = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            return Guid.Parse(userIdentifier);
        }
    }
}
