using Ardalis.Specification;
using BusinessLogic.Entities.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLogic.Specifications
{
    internal static class AdvertValueSpecs
    {
        public class GetAdvertValues : Specification<AdvertValue>
        {
            public GetAdvertValues(int advertId) =>
                Query.Where(x => x.AdvertId == advertId);
        }
    }
}
