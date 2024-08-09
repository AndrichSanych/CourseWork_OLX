using BusinessLogic.Interfaces;
using BusinessLogic.Models.AccountModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork_OLX.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class  AccountsController(IAccountService accountsService) : ControllerBase
    {
        private readonly IAccountService accountsService = accountsService;

        [AllowAnonymous]
        [HttpGet("user")]
        public async Task<IActionResult> GetUser([FromQuery]string userId) => Ok(await accountsService.GetUserAsync(userId));
       

        [AllowAnonymous]
        [HttpPost("user/register")]
        public async Task<IActionResult> UserRegister([FromForm] RegisterUserModel model)
        {
            await accountsService.RegisterUserAsync(model);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] AuthRequest model) => Ok(await accountsService.LoginAsync(model));

        [Authorize(Roles = "User")]
        [HttpPost("togglefavorite/{advertId:int}")]
        public async Task<IActionResult> ToggleFavorite([FromRoute] int advertId)
        {
            await  accountsService.ToggleFavoriteAdvert(advertId, HttpContext.User);
            return Ok();
        }
    }
}
