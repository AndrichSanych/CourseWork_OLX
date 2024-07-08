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
    internal class AdvertSpecs
    {
        public class GetAll : Specification<Advert>
        {
            public GetAll() => Query.Where(x => true)
                .Include(x=>x.Category)
                .Include(x=>x.City);
        }
    }
}
