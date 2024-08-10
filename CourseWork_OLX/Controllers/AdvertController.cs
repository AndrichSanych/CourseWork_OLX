using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Models.AdvertModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork_OLX.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertService advertService;

        public AdvertController(IAdvertService advertService)
        {
            this.advertService = advertService;
        }

        [AllowAnonymous]
        [HttpGet("adverts")]
        public async Task<IActionResult> GetAll() => Ok(await advertService.GetAllAsync());

        [AllowAnonymous]
        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) => Ok(await advertService.GetByIdAsync(id));

        [AllowAnonymous]
        [HttpGet("get")]
        public async Task<IActionResult> GetByUserEmail([FromQuery] string email) => Ok(await advertService.GetByUserEmailAsync(email));

        
        [AllowAnonymous]
        [HttpGet("images/{id:int}")]
        public async Task<IActionResult> GetAdvertImages([FromRoute] int id) => Ok(await advertService.GetImagesAsync(id));

        [AllowAnonymous]
        [HttpGet("vip/{count:int}")]
        public async Task<IActionResult> GetVipAdverts([FromRoute] int count) => Ok(await advertService.GetVIPAsync(count));

        [AllowAnonymous]
        [HttpPost("adverts")]
        public async Task<IActionResult> GetAdverts([FromBody] int[] ids) => Ok(await advertService.getAdvertsAsync(ids));


        [AllowAnonymous]
        [HttpPost("findadverts")]
        public async Task<IActionResult> GetByFilter([FromForm] AdvertSearchModel filter) => Ok(await advertService.GetByFilterAsync(filter));


        [AllowAnonymous]
        [HttpPost("favadverts")]
        public async Task<IActionResult> GetFavoriteByFilter([FromForm] FavoriteAdvertSearchModel filter) => Ok(await advertService.GetByFilterAsync(filter));

        [Authorize(Roles = "User")]
        [HttpPost("useradverts")]
        public async Task<IActionResult> GetUserAdverts([FromForm] UserAdvertSearchModel filter) => Ok(await advertService.GetByFilterAsync(filter));


        [Authorize(Roles = "User")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] AdvertCreationModel adverModel)
        {
            await advertService.CreateAsync(adverModel);
            return Ok();
        }

        [Authorize(Roles = "User")]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] AdvertCreationModel model)
        {
            await advertService.UpdateAsync(model);
            return Ok();
        }


        [Authorize(Roles = "User")]
        [HttpDelete("delete/{Id:int}") ]
        public async Task<IActionResult> DeleteFeedback([FromRoute] int Id)
        {
            await advertService.DeleteAsync(Id);
            return Ok();
        }
    }
}
