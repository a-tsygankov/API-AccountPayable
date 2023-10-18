using System;
using AccountPayable.Core.Entities;

namespace AccountPayable.Core.Interfaces
{
	public interface IVendorRepository : IRepository<Vendor>, IDisposable
	{
	}
}

