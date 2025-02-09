﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBookRental.Domain.Entities;
using MyBookRental.Domain.Security.Tokens;
using MyBookRental.Domain.Services.LoggedUser;
using MyBookRental.Infrastructure.DataAccess;

namespace MyBookRental.Infrastructure.Services.LoggedUser
{
    public class LoggedUser : ILooggedUser
    {
        private readonly MyBookRentalDbContext _dbContext;
        private readonly ITokenProvider _tokenProvider;

        public LoggedUser(MyBookRentalDbContext dbContext, ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider;
        }
        public async Task<User> User()
        {
            var token = _tokenProvider.Value();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            var userIdentifier = Guid.Parse(identifier);

            return await _dbContext.Users.AsNoTracking().FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
        }
    }
}
