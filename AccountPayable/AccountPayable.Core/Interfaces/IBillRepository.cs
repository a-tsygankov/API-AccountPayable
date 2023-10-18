using System;
using AccountPayable.Core.Entities;

namespace AccountPayable.Core.Interfaces
{
	public interface IBillRepository : IRepository<Bill>, IDisposable
	{
	}
}

