using Dapper;
using Ecommerce.Order.CrossCutting.Database;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ecommerce.Order.Repository.Context
{
    public class UnitOfWorkQuery : IRepositoryQuery
    {
        private readonly string _connectionString;

        public UnitOfWorkQuery(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Implement interface IRepositoryQuery
        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            using (var _connection = GetConnection())
                return (await _connection.QueryAsync<T>(sql, param, transaction)).AsList();
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            using (var _connection = GetConnection())
                return await _connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }
        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            using (var _connection = GetConnection())
                return await _connection.QuerySingleAsync<T>(sql, param, transaction);
        }
        #endregion

        private IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}