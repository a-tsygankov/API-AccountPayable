using System;
using AccountPayable.Core.Entities;

namespace AccountPayable.Core.Interfaces
{
	public interface IPaymentMethodRepository : IRepository<PaymentMethod>
	{
	}
}

