using BusinessLogic.Interfaces;
using BusinessLogic.Models.AdvertModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork_OLX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertService advertService;

        public AdvertController(IAdvertService advertService)
        {
            this.advertService = advertService;
        }

        [HttpGet("getadverts")]
        public async Task<IActionResult> GetAll() => Ok(await advertService.GetAllAsync());

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] AdvertCreationModel adverModel)
        {
            await advertService.CreateAsync(adverModel);
            return Ok();
        }
    }
}
