using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace TwitterServer.Auth
{
    public class IdentityService : IIdentityService
    {
        public Task<string> Authenticate(string email, string userId)
        {
            return Task.FromResult(GenerateAccessToken(email, userId));
        }

        private string GenerateAccessToken(string email, string userId)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("secretsecretsecret"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, email)
            };  

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                "issuer",
                "audience",
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
