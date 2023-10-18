using System.Data.Common;
using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using AccountPayable.Sql.Queries;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AccountPayable.Core.Repos
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ILogger<BaseRepository<Payment>> logger, IConfiguration configuration) : base(logger, configuration)
        {
        }

        public async Task<IReadOnlyList<Payment>> GetByBillIdAsync(long billId)
        {
            var result = await _connection.QueryAsync<Payment>(PaymentQueries.PaymentByBillId, new { BillId = billId });

            return result.ToList();
        }
    }
}

