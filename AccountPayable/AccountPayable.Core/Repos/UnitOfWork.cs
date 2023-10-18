using System;
using AccountPayable.Core.Interfaces;

namespace AccountPayable.Core.Repos
{
	public class UnitOfWork : IUnitOfWork

	{
		public UnitOfWork()
		{
		}

        public IVendorRepository Vendors => throw new NotImplementedException();

        public IPaymentMethodRepository PaymentMethods => throw new NotImplementedException();

        public IBillRepository Bills => throw new NotImplementedException();

        public IPaymentRepository Payments => throw new NotImplementedException();
    }
}

