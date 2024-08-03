using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.AdvertModels
{
    public class AdvertCreationModel
    {
        public string UserId { get; set; } = string.Empty;
        public int CityId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsNew { get; set; }
        public bool IsVip { get; set; }
        public bool IsContractPrice { get; set; }
        public decimal Price { get; set; }
        public List<IFormFile> ImageFiles { get; set; } = [];
        public int[] FilterValues { get; set; } = [];
    }
}
