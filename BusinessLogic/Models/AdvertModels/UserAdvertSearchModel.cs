using BusinessLogic.Entities;
using System.Linq.Expressions;


namespace BusinessLogic.Models.AdvertModels
{
    public class UserAdvertSearchModel : SearchModel<Advert>
    {
        public string UserId { get; set; } = string.Empty;
        public override Expression<Func<Advert, bool>> GetExpression() =>
            z => z.UserId == UserId;

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
