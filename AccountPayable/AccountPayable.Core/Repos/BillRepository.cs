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
    public class PaymentRepository : IPaymentRepository
	{
        private readonly ILogger<PaymentRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public PaymentRepository(ILogger<PaymentRepository> logger, IConfiguration configuration)
		{
            this._logger = logger;
            this._configuration = configuration;

            /* @todo Convert to inject existing IDbConnection or IDbContext */
            _connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            _connection.Open();
        }

        public async Task<IReadOnlyList<Payment>> GetAllAsync()
        {
            var result = await _connection.QueryAsync<Payment>(PaymentQueries.AllPayment);

            _logger.LogDebug($"All Payments requested. Retrieved: {result?.Count()}");

            return result.ToList();
        }

        public async Task<Payment> GetByIdAsync(long id)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<Payment>(PaymentQueries.PaymentById, new { Id = id });

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Payment [{id}] requested. Retrieved: {result?.ToDump()}");
            }
            return result;
        }

        public async Task<string> AddAsync(Payment entity)
        {
            var result = await _connection.ExecuteAsync(PaymentQueries.AddPayment, entity);

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Payment [{entity?.ToDump()}] added.");
            }
            return result.ToString();
        }

        public async Task<string> DeleteAsync(long id)
        {
            var result = await _connection.ExecuteAsync(PaymentQueries.DeletePayment, new { Id = id });

            _logger.LogDebug($"Payment [{id}] deleteded.");

            return result.ToString();
        }





        public async Task<string> UpdateAsync(Payment entity)
        {
            var result = await _connection.ExecuteAsync(PaymentQueries.UpdatePayment, entity);
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Payment updated to [{entity?.ToDump()}].");
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

