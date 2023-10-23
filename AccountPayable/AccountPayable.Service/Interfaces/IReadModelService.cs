using AccountPayable.Core.Entities;
using AccountPayable.Service.ReadModels;

namespace AccountPayable.Service.Interfaces
{
    public interface IReadModelService
	{
        public Task<IReadOnlyList<BillRM>> GetBillReadModelAsync(IList<Bill> bills);
        public Task<IReadOnlyList<PaymentRM>> GetPaymentReadModelAsync(IList<Payment> payments);

    }
}

