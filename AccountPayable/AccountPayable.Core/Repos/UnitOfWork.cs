using AccountPayable.Core.Interfaces;

namespace AccountPayable.Core.Repos
{
    public class UnitOfWork : IUnitOfWork

	{
        public IVendorRepository Vendors { get; }

        public IPaymentMethodRepository PaymentMethods { get; }

        public IBillRepository Bills { get; }

        public IPaymentRepository Payments { get; }


        public UnitOfWork(IVendorRepository vendorRepository,
                          IPaymentMethodRepository paymentMethodRepository,
                          IPaymentRepository paymentRepository,
                          IBillRepository billRepository)
		{
            Vendors = vendorRepository;
            PaymentMethods = paymentMethodRepository;
            Payments = paymentRepository;
            Bills = billRepository;
		}

    }
}

