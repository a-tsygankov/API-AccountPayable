using System.Data;
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
    public class VendorRepository : IVendorRepository
	{
        private readonly ILogger<VendorRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public VendorRepository(ILogger<VendorRepository> logger, IConfiguration configuration)
		{
            this._logger = logger;
            this._configuration = configuration;

            /* @todo Convert to inject existing IDbConnection or IDbContext */
            _connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            _connection.Open();
        }

        public async Task<IReadOnlyList<Vendor>> GetAllAsync()
        {
            var result = await _connection.QueryAsync<Vendor>(VendorQueries.AllVendor);

            _logger.LogDebug($"All Vendors requested. Retrieved: {result?.Count()}");

            return result.ToList();
        }

        public async Task<Vendor> GetByIdAsync(long id)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<Vendor>(VendorQueries.VendorById, new { Id = id });

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Vendor [{id}] requested. Retrieved: {result?.ToDump()}");
            }
            return result;
        }

        public async Task<string> AddAsync(Vendor entity)
        {
            var result = await _connection.ExecuteAsync(VendorQueries.AddVendor, entity);

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Vendor [{entity?.ToDump()}] added.");
            }
            return result.ToString();
        }

        public async Task<string> DeleteAsync(long id)
        {
            var result = await _connection.ExecuteAsync(VendorQueries.DeleteVendor, new { VendorId = id });

            _logger.LogDebug($"Vendor [{id}] deleteded.");

            return result.ToString();
        }





        public async Task<string> UpdateAsync(Vendor entity)
        {
            var result = await _connection.ExecuteAsync(VendorQueries.UpdateVendor, entity);
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Vendor updated to [{entity?.ToDump()}].");
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

