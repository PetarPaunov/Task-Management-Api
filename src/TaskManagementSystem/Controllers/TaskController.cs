namespace TaskManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Http;
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
    }
}
