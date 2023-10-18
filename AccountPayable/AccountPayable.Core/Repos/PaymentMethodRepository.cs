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
    public class PaymentMethodRepository : IPaymentMethodRepository
	{
        private readonly ILogger<PaymentMethodRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public PaymentMethodRepository(ILogger<PaymentMethodRepository> logger, IConfiguration configuration)
		{
            this._logger = logger;
            this._configuration = configuration;

            /* @todo Convert to inject existing IDbConnection or IDbContext */
            _connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            _connection.Open();
        }

        public async Task<IReadOnlyList<PaymentMethod>> GetAllAsync()
        {
            var result = await _connection.QueryAsync<PaymentMethod>(PaymentMethodQueries.AllPaymentMethod);

            _logger.LogDebug($"All PaymentMethods requested. Retrieved: {result?.Count()}");

            return result.ToList();
        }

        public async Task<PaymentMethod> GetByIdAsync(long id)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<PaymentMethod>(PaymentMethodQueries.PaymentMethodById, new { Id = id });

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"PaymentMethod [{id}] requested. Retrieved: {result?.ToDump()}");
            }
            return result;
        }

        public async Task<string> AddAsync(PaymentMethod entity)
        {
            var result = await _connection.ExecuteAsync(PaymentMethodQueries.AddPaymentMethod, entity);

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"PaymentMethod [{entity?.ToDump()}] added.");
            }
            return result.ToString();
        }

        public async Task<string> DeleteAsync(long id)
        {
            var result = await _connection.ExecuteAsync(PaymentMethodQueries.DeletePaymentMethod, new { PaymentMethodId = id });

            _logger.LogDebug($"PaymentMethod [{id}] deleteded.");

            return result.ToString();
        }





        public async Task<string> UpdateAsync(PaymentMethod entity)
        {
            var result = await _connection.ExecuteAsync(PaymentMethodQueries.UpdatePaymentMethod, entity);
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"PaymentMethod updated to [{entity?.ToDump()}].");
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

