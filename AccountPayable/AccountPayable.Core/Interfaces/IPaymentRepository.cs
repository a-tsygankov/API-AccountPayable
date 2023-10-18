using System;
using AccountPayable.Core.Entities;

namespace AccountPayable.Core.Interfaces
{
	public interface IPaymentRepository : IRepository<Payment>
	{
        Task<IReadOnlyList<Payment>> GetByBillIdAsync(long billId);

    }
}

