using System;
namespace AccountPayable.Service.Interfaces
{
	public interface IUnitOfWork
	{
        public IVendorRepository Vendors { get; }

        public IPaymentMethodRepository PaymentMethods { get; }

        public IBillRepository Bills { get; }

        public IPaymentRepository Payments { get; }
    }
}

