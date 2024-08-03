using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork_OLX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly IFilterService filterService;

        public FilterController(IFilterService filterService)
        {
            this.filterService = filterService;
        }

        [HttpGet("advert-values/{advertId:int}")]
        public async Task<IActionResult> GetAdvertValues([FromRoute] int advertId) => Ok(await filterService.GetAdvertValues(advertId));

        [HttpGet("category-filters/{categoryId:int}")]
        public async Task<IActionResult> GetCategoryFilters([FromRoute] int categoryId) => Ok(await filterService.GetCategoryFilters(categoryId));
    }
}
