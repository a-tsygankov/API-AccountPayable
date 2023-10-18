using System;
using AccountPayable.Core.Entities;

namespace AccountPayable.Service.Interfaces
{
	public interface IVendorRepository : IRepository<Vendor>, IDisposable
	{
	}
}

