using System.Data.Common;
using AccountPayable.Core.Interfaces;

namespace AccountPayable.Core.Repos
{
    /**
     * @todo rework this class to share IDbConnection instance between all repos
     */
    public class UnitOfWork : IUnitOfWork, IDisposable

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

        /*@todo Use IAsyncDisposable with awaiting all running tasks finished*/
        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // disposing repos
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

