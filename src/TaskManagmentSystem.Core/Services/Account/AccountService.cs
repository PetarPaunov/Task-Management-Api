namespace TaskManagementSystem.Core.Services.Account
{
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography;
    using TaskManagementSystem.Core.Contracts.Account;
    using TaskManagementSystem.Core.Models.AccountModels;
    using TaskManagementSystem.Infrastructure.Common;
    using TaskManagementSystem.Infrastructure.Models;

    public class AccountService : IAccountService
    {
        private readonly IRepository repository;
        private readonly IJwtService jwtService;

        public AccountService(IRepository repository, IJwtService jwtService)
        {
            this.repository = repository;
            this.jwtService = jwtService;
        }

        public async Task RegisterUserAsync(ApplicationUserRegisterModel request)
        {
            this.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new ApplicationUser()
            {
                Username = request.Username,
                Email = request.Email,
                Password = passwordHash,
                PasswordSalt = passwordSalt
            };

            await this.repository.AddAsync<ApplicationUser>(user);
            await this.repository.SaveChangesAsync();
        }

        public async Task<string> LoginUserAsync(ApplicationUserLoginModel request)
        {
            var user = await repository.AllReadonly<ApplicationUser>()
                .FirstOrDefaultAsync(x => x.Username == request.Username);

            if (user == null)
            {
                throw new ArgumentException("User does not exist!");
            }

            if (!VerifyPasswordHash(request.Password, user.Password, user.PasswordSalt))
            {
                throw new ArgumentException("Something went wrong!");
            }

            var token = this.jwtService.CreateJwtToken(user);

            return token;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var loginPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return loginPasswordHash.SequenceEqual(passwordHash);
        }
    }
}