using Ardalis.Specification;
using BusinessLogic.Entities;
using System;

namespace BusinessLogic.Specifications
{
    internal static class AreaSpecs
    {
        public class GetAll : Specification<Area>
        {
            public GetAll() => Query.Where(x => true);
        }

        public class GetByCityId : Specification<Area>
        {
            public GetByCityId(int id) => Query.Where(x =>x.Cities.Any(z => z.Id == id));
        }
    }
}
