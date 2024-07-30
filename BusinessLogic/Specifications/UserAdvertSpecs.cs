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
    internal static class UserAdvertSpecs
    {
        public class GetAll : Specification<UserAdvert>
        {
            public GetAll() => Query.Where(x => true);
        }

        public class GetById : Specification<UserAdvert>
        {
            public GetById(int id) => Query.Where(x => x.Id == id );
        }

        public class GetByUserIdAdvertId : Specification<UserAdvert>
        {
            public GetByUserIdAdvertId(int advertId,string userId) => Query.Where(x => x.UserId == userId && x.AdvertId == advertId);
        }
    }
}
