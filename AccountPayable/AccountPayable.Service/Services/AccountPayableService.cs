using System.Diagnostics;
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
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<AccountPayableService> _logger;
        private BillCanBeMarkedPaidValidator _billCanBeMarkedAsPaid;
             
        public AccountPayableService(IUnitOfWork unitOfWork, ILogger<AccountPayableService> logger)
		{
            this._unitOfWork = unitOfWork;
            _logger = logger;
            _billCanBeMarkedAsPaid = new BillCanBeMarkedPaidValidator(_unitOfWork.Payments);
           

            _logger.LogInformation("Started");
		}

        public Task<string> CreatePaymentAsync(Payment entity)
        {
            _logger.LogDebug($"Create payment: {entity.ToDump()}");

            // @todo implement idempotency check
            return  _unitOfWork.Payments.AddAsync(entity);
        }

        public async Task<IReadOnlyList<Bill>> QueryBillsAsync(long? accountId, long? vendorId, bool isPaid = false)
        {
            var timer = new Stopwatch();
            _logger.LogDebug($"Query bills, accountId: {accountId}, vendorId: {vendorId}, isPaid: {isPaid}");

            var bills = await _unitOfWork.Bills.GetAllAsync();
            var result = bills
                .Where(x => IsMatch(x, accountId, vendorId, isPaid))
                .ToList();

            _logger.LogDebug($"Found {result.Count}, ms: {timer.ElapsedMilliseconds}");

            return result;
        }

        public async Task<string> MarkBillsAsPaidAsync(IReadOnlyList<long> billIds)
        {
            var timer = new Stopwatch();
            _logger.LogDebug($"Marking as paid: {billIds.ToDump()}");

            var billsToUpdate = billIds
                .ToAsyncEnumerable()
                .SelectAwait(async x => await _unitOfWork.Bills.GetByIdAsync(x));

            var tasks = new List<Task<string>>();
            await foreach (var bill in billsToUpdate)
            {
                if (!_billCanBeMarkedAsPaid.IsValid(bill))
                {
                    // @todo graceful validation errors
                    throw new Exception($"Bill {bill.ToDump()} cannot be marked as paid");
                }

                bill.Paid = true;
                tasks.Add(_unitOfWork.Bills.UpdateAsync(bill));
            }

            Task.WaitAll(tasks.ToArray());

            _logger.LogDebug($"Marked as paid {billIds.Count}, ms: {timer.ElapsedMilliseconds}");

            return "Bills are marked as paid";
        }
        /**
        * Crud methods implementatoion for Bill/Vendor/Payment/etc entities should be in separate services
        */

        private bool IsMatch(Bill bill, long? accountId, long? vendorId, bool? isPaid = false)
        {
            if (bill == null)
                throw new ArgumentNullException();

            if (accountId != null && bill.AccountId != accountId)
                return false;
            if (vendorId != null && bill.VendorId != vendorId)
                return false;
            if (isPaid != null && bill.Paid != isPaid)
                return false;

            return true;
        }
    }
}

