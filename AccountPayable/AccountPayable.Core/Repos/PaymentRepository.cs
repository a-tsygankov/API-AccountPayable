using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AccountPayable.Core.Repos
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ILogger<BaseRepository<Payment>> logger, IConfiguration configuration) : base(logger, configuration)
        {
        }
    }
}

