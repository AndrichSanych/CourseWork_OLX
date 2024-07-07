using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork_OLX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService citiesService;

        public CitiesController(ICitiesService citiesService)
        {
            this.citiesService = citiesService;
        }

        [AllowAnonymous]
        [HttpGet("getcities")]
        public async Task<IActionResult> GetAllCountries()
        {
            return Ok(await citiesService.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpGet("getbyid/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            return Ok(await citiesService.GetByIdAsync(id));
        }

        [AllowAnonymous]
        [HttpGet("getbyareaid/{id:int}")]
        public async Task<IActionResult> GetByAreaId([FromRoute] int id)
        {
            return Ok(await citiesService.GetByAreaIdAsync(id));
        }
    }
}
