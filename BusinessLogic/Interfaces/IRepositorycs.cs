using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIDAsync(object id);
        Task InsertAsync(TEntity entity);
        void Delete(object id);
        Task DeleteAsync(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        Task SaveAsync();
        Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification);
        Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification);
    }
}
