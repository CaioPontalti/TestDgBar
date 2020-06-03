using Clearsale.Domain;
using Clearsale.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Clearsale.Api.Service
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["token:secret"]);

            List<Claim> listClaims = new List<Claim>();


            listClaims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
            listClaims.Add(new Claim(ClaimTypes.Email,user.Email));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(listClaims);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(Convert.ToUInt32(_configuration["token:expiration"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return Task.FromResult(encodedToken);
        }
    }
}
