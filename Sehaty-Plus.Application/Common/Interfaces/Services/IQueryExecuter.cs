using static Dapper.SqlMapper;

namespace Sehaty_Plus.Application.Common.Interfaces.Services
{
    public interface IQueryExecuter
    {
        Task<IEnumerable<T>> Query<T>(string sql, object? param = null);
        Task<T?> QueryFirstOrDefault<T>(string sql, object? param = null);
        Task<T?> ExecuteScalar<T>(string sql, object? param = null);
        Task<T?> QueryMultiple<T>(string sql, Func<GridReader, Task<T?>> mapResultSets, object? param = null);
    }

}
