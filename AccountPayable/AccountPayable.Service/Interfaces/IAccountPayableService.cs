using AccountPayable.Core.Entities;

namespace AccountPayable.Service.Interfaces
{
    public interface IAccountPayableService
	{
        Task<string> CreatePaymentAsync(Payment entity);

        Task<string> MarkBillsAsPaidAsync(IReadOnlyList<long> billIds);

        Task<IReadOnlyList<Bill>> GetAllBillAsync(bool isPaid);

        /**
         * Crud methods implementatoion for other entities
         */
    }
}

