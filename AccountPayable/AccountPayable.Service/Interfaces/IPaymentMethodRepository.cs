using System;
using AccountPayable.Core.Entities;

namespace AccountPayable.Service.Interfaces
{
	public interface IPaymentMethodRepository : IRepository<PaymentMethod>, IDisposable
	{
	}
}

