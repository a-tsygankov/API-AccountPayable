using AccountPayable.Core.Entities;
using AccountPayable.Sql.Queries;

namespace AccountPayable.Core.Repos.Factories
{
    public static class CrudQueryFactory
	{
		public static ICrudQueries GetQueries<T>()
		{
			switch (typeof(T).Name)
			{
                case nameof(Bill): return new BillQueries();
                case nameof(Vendor): return new VendorQueries();
                case nameof(Payment): return new PaymentQueries();
                case nameof(PaymentMethod): return new PaymentMethodQueries();
				default: throw new ArgumentException("not supported entity type");
            }
        }
	}
}

