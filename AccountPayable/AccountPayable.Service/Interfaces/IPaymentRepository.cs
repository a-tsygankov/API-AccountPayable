using System;
using AccountPayable.Core.Entities;

namespace AccountPayable.Service.Interfaces
{
	public interface IPaymentRepository : IRepository<Payment>, IDisposable
	{
	}
}

