namespace TaskManagementSystem.Core.Contracts.UserTask
{
    using TaskManagementSystem.Core.Models.UserTaskModels;

    public interface ITaskService
    {
        Task AddTaskAsync(AddTaskModel request, Guid userId);
        Task<IEnumerable<GetTaskModel>> GetTaskAsync(Guid userId);
        Task MoveTaskAsync(Guid id);
        Task FinishTaskAsync(Guid id);
    }
}
