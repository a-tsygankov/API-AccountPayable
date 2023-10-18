using System.Collections.Concurrent;
using AccountPayable.Core.Interfaces;

namespace AccountPayable.Service.Tests.Mocks
{
    public class MockBaseRepository<T> : IRepository<T> where T : IEntity
    {
        IDictionary<long, T> _list = new ConcurrentDictionary<long, T>();

		public MockBaseRepository()
		{
		}

        public async Task<string> AddAsync(T entity)
        {
            // no extra checks -- early failure !!!

            _list[entity.Id] = entity;
            return "";
        }

        public async Task<string> DeleteAsync(long id)
        {
            _list.Remove(id);
            return "";
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return _list.Values.ToList();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return _list[id];
        }

        public async Task<string> UpdateAsync(T entity)
        {
            _list[entity.Id] = entity;
            return "";
        }
    }
}

