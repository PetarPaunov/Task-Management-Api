namespace TaskManagementSystem.Core.Contracts.Account
{
    using System.IdentityModel.Tokens.Jwt;
    using TaskManagementSystem.Infrastructure.Models;

    public interface IJwtService
    {
        string CreateJwtToken(ApplicationUser user);
        JwtSecurityToken ValidateJwtToke(string jwt);
    }
}