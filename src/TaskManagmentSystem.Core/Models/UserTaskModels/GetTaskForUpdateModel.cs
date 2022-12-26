namespace TaskManagementSystem.Core.Models.UserTaskModels
{
    using TaskManagementSystem.Infrastructure.Emuns;

    public class GetTaskForUpdateModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public State State { get; set; }

        public Importance Importance { get; set; }
    }
}
