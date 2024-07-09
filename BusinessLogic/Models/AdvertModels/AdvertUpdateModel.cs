using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.AdvertModels
{
    public class AdvertUpdateModel : AdvertCreationModel
    {
        public int Id { get; set; }
        public List<string> Images { get; set; } = [];
    }
}
