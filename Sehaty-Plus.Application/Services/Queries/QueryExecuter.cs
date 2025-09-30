using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Sehaty_Plus.Application.Services.Queries
{
    public class QueryExecuter : IQueryExecuter
    {
        private readonly string _connection;
        public QueryExecuter(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("DefaultConnection");
        }

        public async Task<IEnumerable<T>> Query<T>(string sql, object? param = null)
        {
            var result = await ExecuteQuery(connection => connection.QueryAsync<T>(sql, param), param!);

            return result;
        }

        public async Task<T?> QueryFirstOrDefault<T>(string sql, object? param = null)
        {
            var result = await ExecuteQuery(connection => connection.QueryFirstOrDefaultAsync<T>(sql, param), param!);

            return result;
        }
        public Task<T?> ExecuteScalar<T>(string sql, object? param = null)
        {
            return ExecuteQuery(connection => connection.ExecuteScalarAsync<T>(sql, param), param!);
        }
        public async Task<T?> QueryMultiple<T>(string sql, Func<SqlMapper.GridReader, Task<T?>> mapResultSets, object? param = null)
        {
            var result = await ExecuteQuery(async connection =>
            {
                var reader = await connection.QueryMultipleAsync(sql, param);
                var result = await mapResultSets(reader);
                return result;
            }, param!);

            return result;
        }
        private async Task<T> ExecuteQuery<T>(Func<IDbConnection, Task<T>> executerFunc, object param)
        {
            using var connection = new SqlConnection(_connection);
            return await executerFunc(connection);
        }
    }
}
