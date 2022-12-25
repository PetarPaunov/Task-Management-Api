namespace TaskManagementSystem.Core.Contracts.Account
{
    using TaskManagementSystem.Core.Models.AccountModels;

    public interface IAccountService
    {
        Task RegisterUserAsync(ApplicationUserRegisterModel request);
        Task<string> LoginUserAsync(ApplicationUserLoginModel request);
    }
}