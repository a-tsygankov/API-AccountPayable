using System;
using AccountPayable.Core.Interfaces;

namespace AccountPayable.Service.Tests.Mocks
{
	public class MockBaseRepository<T> : IRepository<T> where T : class
    {
		public MockBaseRepository()
		{
		}

        public Task<string> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

