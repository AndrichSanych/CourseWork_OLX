using System.Linq.Expressions;


namespace BusinessLogic.Models
{
   public abstract class  SearchModel<TEntity> where TEntity : class
    {
        public  int Count { get; set; }
        public  int Page { get; set; }
        public  int SortIndex { get; set; }
        public abstract Expression<Func<TEntity, bool>> GetExpression();
        public abstract SortData? GetSortData();

    }
}
