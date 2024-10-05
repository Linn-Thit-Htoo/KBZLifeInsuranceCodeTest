using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.Shared.Services
{
    public class DapperService
    {
        private readonly IConfiguration _configuration;

        public DapperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<T>> QueryAsync<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                using IDbConnection db = GetSqlConnection();
                var lst = await db.QueryAsync<T>(query, parameters, commandType: commandType);

                return lst.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                using IDbConnection db = GetSqlConnection();
                var item = await db.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: commandType);

                return item!;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private MySqlConnection GetSqlConnection() => new(_configuration.GetConnectionString("DbConnection"));
    }
}
