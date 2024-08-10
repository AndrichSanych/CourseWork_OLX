using BusinessLogic.Entities;
using BusinessLogic.Exstensions;
using System.Linq.Expressions;


namespace BusinessLogic.Models.AdvertModels
{  
    public class AdvertSearchModel:SearchModel<Advert>
    {
        public int? CategoryId { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsVip { get; set; }
        public bool? IsContractPrice { get; set; }
        public string? Search { get; set; } = string.Empty;
        public int? CityId { get; set; }
        public int? AreaId { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public int[] FilterValues { get; set; } = [];   
       
        public override Expression<Func<Advert, bool>> GetExpression()
        {
            Expression<Func<Advert, bool>> resultExp = x => true;
            Expression<Func<Advert, bool>> searchExpr = x => x.Title.ToLower().Contains(Search.ToLower());
            Expression<Func<Advert, bool>> categoryExpr = x => x.CategoryId == CategoryId;
            Expression<Func<Advert, bool>> isNewExpr = x => x.IsNew == IsNew;
            Expression<Func<Advert, bool>> isVipExpr = x => x.IsVip == IsVip;
            Expression<Func<Advert, bool>> isContractPriceExpr = x => x.IsContractPrice == IsContractPrice;
            Expression<Func<Advert, bool>> areaExpr = x => x.City.AreaId == AreaId;
            Expression<Func<Advert, bool>> cityExpr = x => x.CityId == CityId;
            Expression<Func<Advert, bool>> priceFromExpr = x => x.Price >= PriceFrom;
            Expression<Func<Advert, bool>> priceToExpr = x => x.Price <= PriceTo;
            Expression<Func<Advert, bool>> filterValuesExpr = x => FilterValues.All(z => x.Values.Any(v => v.FilterValueId == z));


            if (!string.IsNullOrEmpty(Search))
                resultExp = resultExp.AndAlso(searchExpr);
            if (CategoryId != null)
                resultExp = resultExp.AndAlso(categoryExpr);
            if (IsNew != null)
                resultExp = resultExp.AndAlso(isNewExpr);
            if (IsVip != null)
                resultExp = resultExp.AndAlso(isVipExpr);
            if (IsContractPrice != null)
                resultExp = resultExp.AndAlso(isContractPriceExpr);
            if (CityId != null)
                resultExp = resultExp.AndAlso(cityExpr);
            else if (AreaId != null)
                resultExp = resultExp.AndAlso(areaExpr);
            if (PriceFrom != null)
                resultExp = resultExp.AndAlso(priceFromExpr);
            if (PriceTo != null)
                resultExp = resultExp.AndAlso(priceToExpr);
            if (FilterValues != null && FilterValues.Count() > 0)
                resultExp = resultExp.AndAlso(filterValuesExpr);
            return resultExp;
        }

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
