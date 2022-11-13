using ETradeApi.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int second)
        {
            Application.DTOs.Token token = new();

            SymmetricSecurityKey securitykey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(securitykey,SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddSeconds(second);
            JwtSecurityToken securtytoken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires:token.Expiration,
                notBefore:DateTime.UtcNow,
                signingCredentials:signingCredentials
                );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(securtytoken);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
