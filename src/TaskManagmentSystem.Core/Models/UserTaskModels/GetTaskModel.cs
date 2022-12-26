namespace TaskManagementSystem.Core.Models.UserTaskModels
{
    using TaskManagementSystem.Infrastructure.Emuns;

    public class GetTaskModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public State State { get; set; }

        public Importance Importance { get; set; }
    }
}