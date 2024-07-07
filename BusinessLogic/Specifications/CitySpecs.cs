using Ardalis.Specification;
using BusinessLogic.Entities;


namespace BusinessLogic.Specifications
{
    internal static class CitySpecs
    {
        public class GetAll:Specification<City>
        {
            public GetAll() => Query.Where(x => true);
        }

        public class GetByAreaId : Specification<City>
        {
            public GetByAreaId(int id) => Query.Where(x => x.AreaId == id);
        }
    }
}
