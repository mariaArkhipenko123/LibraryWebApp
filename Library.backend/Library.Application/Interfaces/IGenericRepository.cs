using System.Linq.Expressions;

namespace Library.Persistense.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        Task<TEntity> GetByID(object id);
        void Update(TEntity entityToUpdate);
        Task Insert(TEntity entity);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
    }
}