﻿using System.Data;
using System.Data.SqlClient;
using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using AccountPayable.Core.Util;
using AccountPayable.Sql.Queries;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static Dapper.SqlMapper;

namespace AccountPayable.Core.Repos
{
    public class BillRepository : IBillRepository
	{
        private readonly ILogger<BillRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public BillRepository(ILogger<BillRepository> logger, IConfiguration configuration)
		{
            this._logger = logger;
            this._configuration = configuration;

            /* @todo Convert to inject existing IDbConnection or IDbContext */
            _connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            _connection.Open();
        }

        public async Task<IReadOnlyList<Bill>> GetAllAsync()
        {
            var result = await _connection.QueryAsync<Bill>(BillQueries.AllBill);

            _logger.LogDebug($"All bills requested. Retrieved: {result?.Count()}");

            return result.ToList();
        }

        public async Task<Bill> GetByIdAsync(long id)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<Bill>(BillQueries.BillById, new { Id = id });

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Bill [{id}] requested. Retrieved: {result?.ToDump()}");
            }
            return result;
        }

        public async Task<string> AddAsync(Bill entity)
        {
            var result = await _connection.ExecuteAsync(BillQueries.AddBill, entity);

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Bill [{entity?.ToDump()}] added.");
            }
            return result.ToString();
        }

        public async Task<string> DeleteAsync(long id)
        {
            var result = await _connection.ExecuteAsync(BillQueries.DeleteBill, new { Id = id });

            _logger.LogDebug($"Bill [{id}] deleteded.");

            return result.ToString();
        }





        public async Task<string> UpdateAsync(Bill entity)
        {
            var result = await _connection.ExecuteAsync(BillQueries.UpdateBill, entity);
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Bill updated to [{entity?.ToDump()}].");
            }
            return result.ToString();
        }

        /*@todo Use IAsyncDisposable with awaiting all running tasks finished*/
        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _connection.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

