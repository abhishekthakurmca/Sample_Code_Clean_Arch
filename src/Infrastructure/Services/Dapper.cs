using Anubis.Application.Common.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Anubis.Infrastructure.Services
{
    public class Dapper : IDapper
    {
        private readonly IConfiguration _config;
        private readonly string CS;

        public Dapper(IConfiguration config)
        {
            _config = config;
            CS = _config.GetConnectionString("DefaultConnection");
        }
        public void Dispose()
        {

        }

        public async Task<IEnumerable<T>> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using var db = new SqlConnection(CS);
            try
            {
                await db.OpenAsync();
                return await db.QueryAsync<T>(sp, parms, commandType: commandType);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }
    }
}
