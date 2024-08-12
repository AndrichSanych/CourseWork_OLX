using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.AdvertModels
{
    public class FavoriteAdvertSearchModel : SearchModel<Advert>
    {
        public string UserId { get; set; } = string.Empty;
        
        public override Expression<Func<Advert, bool>> GetExpression() => 
            x => x.UserFavouriteAdverts.Any(z=>z.UserId == UserId);
        
        public override SortData? GetSortData()
        {
            return SortIndex switch
            {
                1 => new SortData(x => x.Date, true),
                2 => new SortData(x => x.Price, true),
                3 => new SortData(x => x.Price, false),
                _ => null,
            };
        }
    }
}
