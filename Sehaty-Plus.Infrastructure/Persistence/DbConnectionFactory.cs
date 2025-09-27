using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sehaty_Plus.Application.Interfaces;
using System.Data;

namespace Sehaty_Plus.Infrastructure.Data
{
    public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}