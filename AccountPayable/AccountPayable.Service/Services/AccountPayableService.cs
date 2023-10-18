using System;
using AccountPayable.Core.Entities;
using AccountPayable.Service.Interfaces;

namespace AccountPayable.Service.Services
{
	public class AccountPayableService : IAccountPayableService
    {
		public AccountPayableService()
		{
		}

        public Task<string> CreatePaymentAsync(Payment entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Bill>> GetAllBillAsync(bool isPaid)
        {
            throw new NotImplementedException();
        }

        public Task<string> MarkBillsAsPaidAsync(IReadOnlyList<long> billIds)
        {
            throw new NotImplementedException();
        }

 /**
 * Crud methods implementatoion for Bill/Vendor/Payment/etc entities should be in separate services
 */
    }
}

