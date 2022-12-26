namespace TaskManagementSystem.Core.Models.UserTaskModels
{
    public class GetTaskModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string State { get; set; } = null!;

        public string Importance { get; set; } = null!;
    }
}