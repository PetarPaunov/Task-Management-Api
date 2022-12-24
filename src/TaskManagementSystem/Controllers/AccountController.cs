namespace TaskManagementSystem.Controllers
{
    using TaskManagementSystem.Core.Contracts.Account;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using TaskManagementSystem.Core.Models.AccountModels;
    using TaskManagementSystem.Core.Contracts.User;
    using System.Security.Claims;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService service;
        private readonly IJwtService jwtService;
        private readonly IUserService userService;

        public AccountController(IAccountService service, 
                                 IJwtService jwtService,
                                 IUserService userService)
        {
            this.service = service;
            this.jwtService = jwtService;
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(ApplicationUserRegisterModel request)
        {
            if (ModelState.IsValid)
            {
                await service.RegisterUserAsync(request);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(ApplicationUserLoginModel request)
        {
            if (ModelState.IsValid)
            {
                var jwt = await service.LoginUserAsync(request);

                this.Response.Cookies.Append("jwt", jwt, new CookieOptions()
                {
                    HttpOnly = true
                });

                return Ok("Success");
            }

            return BadRequest("Invalid Creadentials");
        }

        [HttpGet("user")]
        public async Task<IActionResult> User()
        {
            try
            {
                var jwt = this.Request.Cookies["jwt"];
                var token = this.jwtService.ValidateJwtToke(jwt);

                var userId = Guid.Parse(token.Issuer);
                var user = await this.userService.GetById(userId);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
    }
}