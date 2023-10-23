using AccountPayable.Core.Entities;

namespace AccountPayable.Core.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
	{
        /**
         * Direct query to retrieve a list of payments related to bill id provided.
         * 
         * <return>List of matching payment or empty list if no payments found</return>
         */
        Task<IReadOnlyList<Payment>> GetByBillIdAsync(long billId);

    }
}

