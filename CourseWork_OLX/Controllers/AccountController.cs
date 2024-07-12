using BusinessLogic.Interfaces;
using BusinessLogic.Models.AccountModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork_OLX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class  AccountsController(IAccountService accountsService) : ControllerBase
    {
        private readonly IAccountService accountsService = accountsService;
        [HttpPost("user/register")]
        public async Task<IActionResult> UserRegister([FromForm] RegisterUserModel model)
        {
            await accountsService.RegisterUserAsync(model);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] AuthRequest model) => Ok(await accountsService.LoginAsync(model));
    }
}
