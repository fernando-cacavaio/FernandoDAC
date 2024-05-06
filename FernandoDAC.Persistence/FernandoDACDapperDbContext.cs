using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FernandoDAC.Persistence
{
    public class FernandoDACDapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public FernandoDACDapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("FernandoDB");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}