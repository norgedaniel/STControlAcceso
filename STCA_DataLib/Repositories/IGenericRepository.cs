using System.Linq.Expressions;

namespace STCA_DataLib.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<bool> DeleteAsync(object id);
        bool Delete(TEntity entityToDelete);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetByIDAsync(object id);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}