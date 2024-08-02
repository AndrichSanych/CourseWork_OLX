using Microsoft.AspNetCore.Http;

namespace CourseWork_OLX.Models.Categories
{
    public class CategoryCreateViewModel
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
