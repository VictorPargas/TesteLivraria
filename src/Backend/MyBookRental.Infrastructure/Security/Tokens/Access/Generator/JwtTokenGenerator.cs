using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MyBookRental.Domain.Security.Tokens;

namespace MyBookRental.Infrastructure.Security.Tokens.Access.Generator
{
    public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
    {
        private readonly uint _expirationTimeMinutes;
        private readonly string _signinKey;

        public JwtTokenGenerator(uint expirationTimeMinutes, string signinKey)
        {
            _expirationTimeMinutes = expirationTimeMinutes;
            _signinKey = signinKey;
        }

        public string Generate(Guid userIdentifier)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, userIdentifier.ToString())
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(_signinKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
