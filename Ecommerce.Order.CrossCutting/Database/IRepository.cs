using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;

namespace Ecommerce.Order.CrossCutting.Database
{
    public interface IRepository<T>
    {
        Task Save(T entity);
        Task SaveRange(IEnumerable<T> entities);
        Task Delete(T entity);
        Task DeleteRange(IEnumerable<T> entities);
        Task Update(T entity);
        Task UpdateRange(IEnumerable<T> entities);
        Task<T> Get(object id);
        Task<IEnumerable<T>> GetAll();
        Task<IDbContextTransaction> CreateTransaction();
        Task<IDbContextTransaction> CreateTransaction(IsolationLevel isolation = System.Data.IsolationLevel.Serializable);
        Task<IEnumerable<T>> GetAllByCriteria(Expression<Func<T, bool>> expression);
        Task<T> GetOneByCriteria(Expression<Func<T, bool>> expression);

        IQueryable<T> GetQueryable();
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> expression);
    }
}
