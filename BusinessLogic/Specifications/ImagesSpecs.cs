using Ardalis.Specification;
using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLogic.Specifications
{
    internal static class ImagesSpecs
    {
        public class GetByAdvertId : Specification<Image>
        {
            public GetByAdvertId(int id) => Query.Where(x => x.AdvertId == id);
        }
    }
}
