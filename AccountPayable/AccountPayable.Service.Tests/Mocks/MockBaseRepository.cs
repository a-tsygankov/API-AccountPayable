using System.Collections.Concurrent;
using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;

namespace AccountPayable.Service.Tests.Mocks
{
    public class MockBaseRepository<T> : IRepository<T> where T : IEntity
    {
        public IDictionary<long, T> Entities = new ConcurrentDictionary<long, T>();

		public MockBaseRepository()
		{
            
		}

        public void Load(T[]? initial = null)
        {
            initial?.ToList().ForEach(x => Entities[x.Id] = x);
        }

        public async Task<string> AddAsync(T entity)
        {
            // @todo no extra checks -- early failure !!!

            Entities[entity.Id] = entity;
            return "";
        }

        public async Task<string> DeleteAsync(long id)
        {
            Entities.Remove(id);
            return "";
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return Entities.Values.ToList();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return Entities[id];
        }

        public async Task<string> UpdateAsync(T entity)
        {
            Entities[entity.Id] = entity;
            return "";
        }
    }



    public class MockVendorRepo : MockBaseRepository<Vendor>, IVendorRepository
    {

    }
    public class MockPaymentMethodRepo : MockBaseRepository<PaymentMethod>, IPaymentMethodRepository
    {

    }
    public class MockBillRepo : MockBaseRepository<Bill>, IBillRepository
    {

    }


    public class MockPaymentRepo : MockBaseRepository<Payment>, IPaymentRepository
    {
        public async Task<IReadOnlyList<Payment>> GetByBillIdAsync(long billId)
        {
            return Entities.Values.Where(x => x.BillId == billId).ToList();
        }
    }
}

