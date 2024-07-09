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


        [HttpGet("adverts")]
        public async Task<IActionResult> GetAll() => Ok(await advertService.GetAllAsync());

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) => Ok(await advertService.GetByIdAsync(id));

        [HttpGet("get")]
        public async Task<IActionResult> GetByUserEmail([FromQuery] string email) => Ok(await advertService.GetByUserEmailAsync(email));


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] AdvertCreationModel adverModel)
        {
            await advertService.CreateAsync(adverModel);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] AdvertUpdateModel model)
        {
            await advertService.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete("delete/{Id:int}")]
        public async Task<IActionResult> DeleteFeedback([FromRoute] int Id)
        {
            await advertService.DeleteAsync(Id);
            return Ok();
        }
    }
}
