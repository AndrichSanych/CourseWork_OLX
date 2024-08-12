using AutoMapper;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Specifications;
using System.Linq.Expressions;


namespace BusinessLogic.Helpers
{
    public class SearchResult<TEntity, TDto>(IRepository<TEntity> repo, IMapper mapper, SearchModel<TEntity> filter) where TEntity : BaseEntity where TDto : class
    {
        public IEnumerable<TDto> Elements { get; set; } = [];
        public int TotalCount { get; set; }

        private readonly IRepository<TEntity> repository = repo;
        private readonly Expression<Func<TEntity, bool>> expression = filter.GetExpression();
        private readonly IMapper mapper = mapper;
        private readonly SearchModel<TEntity> filter = filter;

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
            Elements = mapper.Map<IEnumerable<TDto>>(
                await repository.GetListBySpec(
                    new GenericSpecs.GetByFilter<TEntity>(
                        filter.GetExpression(),
                        filter.GetSortData(),
                        (filter.Page - 1) * filter.Count,
                        filter.Count)));
            return this;
        }
    }
}
