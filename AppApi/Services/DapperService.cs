using Entities;
using Npgsql;
using Dapper;
using System.Data;

namespace AppApi.Services
{
    public class DapperService
    {
        private readonly IConfiguration _configuration;
        

        public DapperService(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }

        public async Task<IDbConnection> GetDbConnection()
        {
            var conn =  new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync();
            return conn;
        }
    }

    
}
