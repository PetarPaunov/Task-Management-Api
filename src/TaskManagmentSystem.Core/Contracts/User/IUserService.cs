namespace TaskManagementSystem.Core.Contracts.User
{
    using TaskManagementSystem.Core.Models.UserModels;

    public interface IUserService
    {
        Task<AuthorizedUserModel> GetById(Guid id);
    }
}
