namespace TaskManagementSystem.Core.Services.User
{
    using TaskManagementSystem.Core.Contracts.User;
    using TaskManagementSystem.Core.Models.UserModels;
    using TaskManagementSystem.Infrastructure.Common;
    using TaskManagementSystem.Infrastructure.Models;

    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<AuthorizedUserModel> GetById(Guid id)
        {
            var user = await this.repository.GetByIdAsync<ApplicationUser>(id);

            if (user == null)
            {
                throw new ArgumentNullException("User Not Found!");
            }

            return new AuthorizedUserModel()
            {
                Id = user.Id,
                UserName = user.Username
            };
        }
    }
}
