using AutoMapper;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Specifications;
using System.Linq.Expressions;


namespace BusinessLogic.Models
{
    public class SearchResult<TEntity,TDto> where TEntity: BaseEntity where TDto:class
    { 
        public IEnumerable<TDto> Elements { get; set; } = [];
        public int TotalCount { get; set; }

        private IRepository<TEntity> repository;
        private Expression<Func<TEntity, bool>> expression;
        private IMapper mapper;
        private SearchModel<TEntity> filter;

        public  SearchResult(IRepository<TEntity> repo,IMapper mapper, SearchModel<TEntity> filter)
        {
            this.repository = repo;
            this.expression = filter.GetExpression();
            this.mapper = mapper;
            this.filter = filter;
        }

        public async Task<SearchResult<TEntity, TDto>> GetResult()
        {
            TotalCount = await repository.CountAsync(expression);
            if (filter.Count > 0)
            {
                int totalPages = (int)Math.Ceiling(TotalCount / (double)filter.Count);
                if (filter.Page > totalPages)
                    filter.Page = totalPages;
            }
            else filter.Count = TotalCount;
            filter.Page = filter.Page <= 0 ? 1 : filter.Page;
            Elements =  mapper.Map<IEnumerable<TDto>>( await repository.GetListBySpec(new GenericSpecs.GetByFilter<TEntity>(filter.GetExpression(), (filter.Page.Value - 1) * filter.Count.Value, filter.Count.Value)));
            return this;
        }
    }
}
