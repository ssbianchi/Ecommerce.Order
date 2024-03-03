using Ecommerce.Order.Domain.Entity.Readonly.Dapper.Order;

namespace Ecommerce.Order.Domain.Entity.Readonly.Repository
{
    public interface IReadonlyRepository
    {
        #region Order
        Task<IEnumerable<DapperOrder>> GetAllOrder();
        #endregion
    }
}
