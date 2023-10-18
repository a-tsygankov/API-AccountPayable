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
    }
}

