using Ardalis.Specification;
using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLogic.Specifications
{
    public static class GenericSpecs
    {
        public class GetByFilter<T> : Specification<T>
        {
            public GetByFilter(Expression<Func<T, bool>> filter, int skip, int take)
            {
                var specification = this as Specification<T>;
                switch (specification)
                {
                    case Specification<Advert> spec:
                        spec.Query.Where(filter as Expression<Func<Advert, bool>>)
                                    .Include(x => x.Category)
                                    .Include(x => x.City)
                                    .Include(x => x.Images)
                                    .Include(x => x.UserFavouriteAdverts)
                                    .Skip(skip)
                                    .Take(take);
                        break;

                    case Specification<Category> spec:
                        spec.Query.Where(filter as Expression<Func<Category, bool>>)
                                      .Include(x => x.Adverts)
                                      .Skip(skip)
                                      .Take(take);
                        break;

                    default:
                        Query.Where(filter)
                        .Skip(skip)
                        .Take(take);
                        break;
                }
                
            }
        }

    }

        
    
}
