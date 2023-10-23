using AccountPayable.Core.Interfaces;
using AccountPayable.Core.Repos;
using Microsoft.Extensions.Logging;

namespace AccountPayable.Service.Tests.Mocks
{
	public class MockUnitOfWork : IUnitOfWork
	{
        private readonly ILogger<MockUnitOfWork> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public MockUnitOfWork(ILogger<MockUnitOfWork> logger)
		{
            this._logger = logger;

            _unitOfWork = new UnitOfWork(
                Fixture.createVendorRepo(),
                Fixture.createPaymentMethodRepo(),
                Fixture.createPaymentRepo(),
                Fixture.createBillRepo());

        }

        public IVendorRepository Vendors => _unitOfWork.Vendors;

        public IPaymentMethodRepository PaymentMethods => _unitOfWork.PaymentMethods;

        public IBillRepository Bills => _unitOfWork.Bills;

        public IPaymentRepository Payments => _unitOfWork.Payments;
    }
}

