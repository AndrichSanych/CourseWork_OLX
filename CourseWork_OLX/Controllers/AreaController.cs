using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork_OLX.Controllers
{
    [Route("api/Areas")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService areaService;

        public AreaController(IAreaService citiesService)
        {
            this.areaService = citiesService;
        }

        [AllowAnonymous]
        [HttpGet("getareas")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await areaService.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpGet("getbyid/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await areaService.GetByIdAsync(id));
        }

        [AllowAnonymous]
        [HttpGet("getbycityid/{id:int}")]
        public async Task<IActionResult> GetByCityId([FromRoute] int id)
        {
            return Ok(await areaService.GetByCityIdAsync(id));
        }
    }
}
