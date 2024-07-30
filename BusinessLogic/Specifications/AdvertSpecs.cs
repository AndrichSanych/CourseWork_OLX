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
                .Include(x=>x.City)
                .Include(x=>x.Images)
                .Include(x=>x.UserFavouriteAdverts);
        }

        public class GetVIP : Specification<Advert>
        {
            public GetVIP(int count) => Query.Where(x => x.IsVip)
                .Include(x => x.Category)
                .Include(x => x.City)
                .Include(x => x.Images)
                .OrderBy(x=> Guid.NewGuid())
                .Include(x => x.UserFavouriteAdverts)
                .Take(count);
        }

        public class GetById : Specification<Advert>
        {
            public GetById(int id) => Query.Where(x => x.Id == id)
                .Include(x => x.Category)
                .Include(x => x.City)
                .Include(x => x.Images)
                .Include(x => x.UserFavouriteAdverts);
        }

        public class GetByIdWithImage : Specification<Advert>
        {
            public GetByIdWithImage(int id) => Query.Where(x => x.Id == id)
                .Include(x => x.Images);
        }

        public class GetByIdUserId : Specification<Advert>
        {
            public GetByIdUserId(string userId) => Query.Where(x => x.UserId == userId)
                .Include(x => x.Category)
                .Include(x => x.City)
                .Include(x => x.Images)
                .Include(x => x.UserFavouriteAdverts);
        }
    }
}
