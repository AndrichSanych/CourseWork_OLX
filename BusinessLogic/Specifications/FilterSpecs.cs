﻿using Ardalis.Specification;
using BusinessLogic.Entities;
using BusinessLogic.Entities.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLogic.Specifications
{
    internal static class FilterSpecs
    {
        public class GetCategoryFilters : Specification<Filter>
        {
            public GetCategoryFilters(int categoryId) => 
                Query
                .Include(x=>x.Values)
                .Include(x=>x.Filters)
                .Where(x =>x.Filters.Any(z=>z.CategoryId==categoryId));
        }

        public class GetAdvertValues : Specification<FilterValue>
        {
            public GetAdvertValues(int advertId) =>
                Query
                .Include(x => x.Values)
                .Where(x => x.Values.Any(z => z.AdvertId == advertId));
        }

    }
}
