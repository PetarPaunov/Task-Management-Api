namespace TaskManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TaskManagementSystem.Core.Contracts.Account;
    using TaskManagementSystem.Core.Contracts.UserTask;
    using TaskManagementSystem.Core.Models.UserTaskModels;

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;
        private readonly IJwtService jwtService;

        public TaskController(ITaskService taskService, IJwtService jwtService)
        {
            this.taskService = taskService;
            this.jwtService = jwtService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddTaskModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var jwt = this.Request.Cookies["jwt"];
                var token = this.jwtService.ValidateJwtToke(jwt);

                var userId = Guid.Parse(token.Issuer);

                await this.taskService.AddTaskAsync(request, userId);

                return Ok();
            }
            catch (ArgumentNullException)
            {
                return Unauthorized();
            }
            catch (ArgumentException e)
            {
                return BadRequest(new
                {
                    message = e.Message,
                });
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var jwt = this.Request.Cookies["jwt"];
                var token = this.jwtService.ValidateJwtToke(jwt);

                var userId = Guid.Parse(token.Issuer);

                var tasks = await this.taskService.GetTaskAsync(userId);

                return Ok(tasks);
            }
            catch (ArgumentNullException)
            {
                return Unauthorized();
            };
        }

        [HttpGet("get-for-update")]
        public async Task<IActionResult> GetForUpdate(string taskId)
        {
            try
            {
                var jwt = this.Request.Cookies["jwt"];
                var token = this.jwtService.ValidateJwtToke(jwt);

                var userId = Guid.Parse(token.Issuer);
                var taskGuid = Guid.Parse(taskId);

                var task = await this.taskService.GetForUpdateAsync(taskGuid, userId);

                return Ok(task);
            }
            catch (ArgumentNullException)
            {
                return Unauthorized();
            }
            catch (ArgumentException e)
            {
                return BadRequest(new
                {
                    message = e.Message,
                });
            };
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateTaskModel request)
        {
            try
            {
                await this.taskService.UpdateTaskAsync(request);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(new
                {
                    message = e.Message,
                });
            };
        }
    }
}