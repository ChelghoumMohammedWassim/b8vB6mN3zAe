using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using b8vB6mN3zAe.Models;
using Microsoft.IdentityModel.Tokens;

namespace b8vB6mN3zAe.Tools
{
    public static class Token
    {
        public static string CreateToken(User user, string tokenKey)
        {
            List<Claim> claims = new List<Claim>
        {
            new Claim("ID", user.ID),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string? DecodeToken(string token, string tokenKey)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(tokenKey);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

                var nameClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "ID");

                return nameClaim?.Value;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}



//dotnet user-secrets set "MySecretKey" "f41a892ec88bf2485a5fd1a308c53fe005fcf5edb03ac635e373b6f37e5c8725ddc1c7f2c89b68dbe9ba434e62d30117c5d1ca1fa0525b777e7f1b95ab92f40b"