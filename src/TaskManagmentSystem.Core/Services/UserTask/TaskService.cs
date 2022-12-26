namespace TaskManagementSystem.Core.Services.UserTask
{
    using Microsoft.EntityFrameworkCore;
    using TaskManagementSystem.Core.Contracts.UserTask;
    using TaskManagementSystem.Core.Models.UserTaskModels;
    using TaskManagementSystem.Infrastructure.Common;
    using TaskManagementSystem.Infrastructure.Emuns;
    using TaskManagementSystem.Infrastructure.Models;

    public class TaskService : ITaskService
    {
        private readonly IRepository repository;

        public TaskService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddTaskAsync(AddTaskModel request, Guid userId)
        {
            var importanceIsValid = Enum.TryParse<Importance>(request.Importance, out var importanceType);

            if (!importanceIsValid)
            {
                throw new ArgumentException("Importance type is not valid!");
            }

            var stateIsValid = Enum.TryParse<State>(request.State, out var stateType);

            if (!stateIsValid)
            {
                throw new ArgumentException("State type is not valid!");
            }

            var task = new UserTask()
            {
                Title = request.Title,
                Description = request.Description,
                Importance = importanceType,
                State = stateType,
                ApplicationUserId = userId
            };

            await this.repository.AddAsync<UserTask>(task);
            await this.repository.SaveChangesAsync();
        }

        public Task FinishTaskAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetTaskModel>> GetTaskAsync(Guid userId)
        {
            var tasks = await repository.AllReadonly<UserTask>()
                .Where(x => x.ApplicationUserId == userId)
                .ToListAsync();

            var tasksList = new List<GetTaskModel>();

            foreach (var task in tasks)
            {
                var importance = task.Importance.ToString();
                var state = task.State.ToString();

                tasksList.Add(new GetTaskModel()
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Importance = importance,
                    State = state
                });
            }

            return tasksList;
        }

        public async Task MoveTaskAsync(Guid taskId, Guid userId, bool hasToPromote)
        {
            var task = await this.repository.All<UserTask>()
                .Where(x => x.Id == taskId && x.ApplicationUserId == userId)
                .FirstOrDefaultAsync();

            if (task == null)
            {
                throw new ArgumentException("The user or task is invalid!");
            }

            if (hasToPromote)
            {
                if ((int)task.State == 10)
                {
                    task.State = State.InProgress;
                }
                else if ((int)task.State == 20)
                {
                    task.State = State.Finished;
                }
            }
            else
            {
                if ((int)task.State == 30)
                {
                    task.State = State.InProgress;
                }
                else if ((int)task.State == 20)
                {
                    task.State = State.Todo;
                }
            }

            await repository.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(UpdateTaskModel request)
        {
            var taskId = Guid.Parse(request.Id);

            var task = await this.repository.GetByIdAsync<UserTask>(taskId);

            var importanceIsValid = Enum.TryParse<Importance>(request.Importance, out var importanceType);

            if (!importanceIsValid)
            {
                throw new ArgumentException("Importance type is not valid!");
            }

            var stateIsValid = Enum.TryParse<State>(request.State, out var stateType);

            if (!stateIsValid)
            {
                throw new ArgumentException("State type is not valid!");
            }

            task.Title = request.Title;
            task.Description = request.Description;
            task.Importance = importanceType;
            task.State = stateType;

            await this.repository.SaveChangesAsync();
        }

        public async Task<GetTaskForUpdateModel> GetForUpdateAsync(Guid taskId, Guid userId)
        {
            var task = await this.repository.AllReadonly<UserTask>()
                .Where(x => x.Id == taskId && x.ApplicationUserId == userId)
                .FirstOrDefaultAsync();

            if (task == null)
            {
                throw new ArgumentException("The user or task is invalid!");
            }

            return new GetTaskForUpdateModel()
            {
                Id = task.Id.ToString(),
                Title = task.Title,
                Description = task.Description,
                Importance = task.Importance,
                State = task.State
            };
        }
    }
}
