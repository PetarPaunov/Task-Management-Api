namespace TaskManagementSystem.Core.Services.Account
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using TaskManagementSystem.Core.Contracts.Account;
    using TaskManagementSystem.Infrastructure.Models;

    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;

        public JwtService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJwtToken(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var jwtConfigToken = this.configuration.GetSection("ApplicationSettings:JWToken").Value;

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtConfigToken));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                issuer: user.Id.ToString(),
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public JwtSecurityToken ValidateJwtToke(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtConfigToken = this.configuration.GetSection("ApplicationSettings:JWToken").Value;

            var key = Encoding.ASCII.GetBytes(jwtConfigToken);

            tokenHandler.ValidateToken(jwt, new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            },
            out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
