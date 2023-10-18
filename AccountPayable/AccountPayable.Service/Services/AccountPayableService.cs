using System;
using System.Linq;
using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using AccountPayable.Service.Interfaces;
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
            var billsToUpdate =  billIds.Select(async x => await _billRepository.GetByIdAsync(x));
            var payments = billIds.ToDictionary(x => (x, await _paymentRepository.GetByIdAsync())
            ValidatePaymentCompleted()
        }

 /**
 * Crud methods implementatoion for Bill/Vendor/Payment/etc entities should be in separate services
 */
    }
}

