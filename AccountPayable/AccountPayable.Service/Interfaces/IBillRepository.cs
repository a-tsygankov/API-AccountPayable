using System;
using AccountPayable.Core.Entities;

namespace AccountPayable.Service.Interfaces
{
	public interface IBillRepository : IRepository<Bill>, IDisposable
	{
	}
}

