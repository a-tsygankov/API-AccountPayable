using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using AccountPayable.Core.Util;
using AccountPayable.Service.Interfaces;
using AccountPayable.Service.Validators;
using Microsoft.Extensions.Logging;

namespace AccountPayable.Service.Services
{
    public class AccountPayableService : IAccountPayableService
    {
        private IVendorRepository _vendorRepository;
        private IPaymentMethodRepository _paymentMethodRepository;
        private IPaymentRepository _paymentRepository;
        private IBillRepository _billRepository;
        private ILogger<AccountPayableService> _logger;
        private BillCanBeMarkedPaidValidator _billCanBeMarkedAsPaid;
             
        public AccountPayableService(IVendorRepository vendorRepository,
                                     IPaymentMethodRepository paymentMethodRepository,
                                     IPaymentRepository paymentRepository,
                                     IBillRepository billRepository,
                                     ILogger<AccountPayableService> logger)
		{
            _vendorRepository = vendorRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _billRepository = billRepository;
            _logger = logger;

            _billCanBeMarkedAsPaid = new BillCanBeMarkedPaidValidator(_paymentRepository);
           

            _logger.LogInformation("Started");
		}

        public Task<string> CreatePaymentAsync(Payment entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Bill>> GetAllBillAsync(bool isPaid)
        {
            throw new NotImplementedException();
        }

        public async Task<string> MarkBillsAsPaidAsync(IReadOnlyList<long> billIds)
        {
            //throw new NotImplementedException();

            var billsToUpdate = billIds
                .ToAsyncEnumerable()
                .SelectAwait(async x => await _billRepository.GetByIdAsync(x));

            await foreach (var bill in billsToUpdate)
            {
                if (!_billCanBeMarkedAsPaid.IsValid(bill))
                {
                    // @todo graceful validation errors
                    throw new Exception($"Bill {bill.ToDump()} cannot be marked as paid");
                }
            }

            return "Bills are marked as paid";
        }

        private object ValidatePaymentCompleted()
        {
            throw new NotImplementedException();
        }

        /**
        * Crud methods implementatoion for Bill/Vendor/Payment/etc entities should be in separate services
        */
    }
}

