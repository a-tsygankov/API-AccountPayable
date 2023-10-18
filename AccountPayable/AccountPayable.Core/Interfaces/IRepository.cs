using System;
namespace AccountPayable.Core.Interfaces
{
	public interface IRepository<T> where T : IEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(long id);

        Task<string> AddAsync(T entity);

        Task<string> UpdateAsync(T entity);

        Task<string> DeleteAsync(long id);
    }
}

