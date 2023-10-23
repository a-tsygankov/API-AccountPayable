using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AccountPayable.Core.Repos
{
    public class PaymentMethodRepository : BaseRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(ILogger<BaseRepository<PaymentMethod>> logger, IConfiguration configuration) : base(logger, configuration)
        {
        }
    }
}

