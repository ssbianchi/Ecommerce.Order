using Ecommerce.Order.Domain.Entity.Readonly.Dapper.Order;
using Ecommerce.Order.Domain.Entity.Readonly.Repository;
using Ecommerce.Order.Repository.Context;
using Ecommerce.Order.Repository.Repository.Options;
using Microsoft.Extensions.Options;

namespace Ecommerce.Order.Repository.Repository
{
    public class ReadonlyRepository : UnitOfWorkQuery, IReadonlyRepository
    {
        public ReadonlyRepository(IOptions<ConnectionStringOptions> options) : base(options.Value.ConnectionString)
        {

        }

        #region Order
        public async Task<IEnumerable<DapperOrder>> GetAllOrder()
        {
            var sql = @"
Select Id
     , Nome
     , Login
     , Password
     , Email
  From Orders";

            //var result = await QueryAsync<DapperOrder>(sql, new { Id = OrderId });
            var result = await QueryAsync<DapperOrder>(sql);
            return result;
        }
        #endregion
    }
}
