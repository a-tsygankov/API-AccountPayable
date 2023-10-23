using AccountPayable.Core.Entities;

namespace AccountPayable.Service.Interfaces
{
    public interface IAccountPayableService
	{
        Task<string> CreatePaymentAsync(long accountId, long billId, decimal amount, long paymentMethodId, DateTime paymentDate);

        Task<string> MarkBillsAsPaidAsync(IReadOnlyList<long> billIds);

        Task<IReadOnlyList<Bill>> QueryBillsAsync(long? accountId, long? vendorId, bool? isPaid = false);

        /**
         * Crud methods implementatoion for other entities
         */
    }
}

