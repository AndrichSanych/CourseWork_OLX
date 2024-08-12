using Ardalis.Specification;
using BusinessLogic.Entities;
using BusinessLogic.Models;
using System.Linq.Expressions;
using System.Timers;

namespace BusinessLogic.Specifications
{
    public static class GenericSpecs
    {
        public class GetByFilter<T> : Specification<T>
        {
            public GetByFilter(Expression<Func<T, bool>> filter, SortData? sortData, int skip, int take)
            {
                var specification = this as Specification<T>;
                switch (specification)
                {
                    case Specification<Advert> spec:
                        spec.Query.Include(x => x.Category)
                                .Include(x => x.City)
                                .ThenInclude(x => x.Area)
                                .Include(x => x.Images)
                                .Include(x => x.UserFavouriteAdverts)
                                .Where(filter as Expression<Func<Advert, bool>> ?? (x => false));
                        if (sortData != null)
                        {
                            if (sortData.Descending)
                            {
                                spec.Query.OrderByDescending(sortData.SortExpr ?? (x => x));
                            }
                            else
                            {
                                spec.Query.OrderBy(sortData.SortExpr ?? (x => x));
                            }
                        }
                        spec.Query.Skip(skip)
                                  .Take(take);
                        break;

                    default:
                        Query.Where(filter);
                        if (sortData != null)
                        {
                            if (sortData.Descending)
                            {
                                Query.OrderByDescending(sortData.SortExpr as Expression<Func<T, object?>> ?? (x => x));
                            }
                            else
                            {
                                Query.OrderBy(sortData.SortExpr as Expression<Func<T, object?>> ?? (x => x));
                            }
                        }
                        Query.Skip(skip)
                             .Take(take);
                        
                        break;
                }
                
            }
        }

    }

        
    
}
