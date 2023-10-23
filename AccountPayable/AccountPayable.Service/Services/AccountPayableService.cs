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

        public async Task<string> CreatePaymentAsync(long accountId, long billId, decimal amount, long paymentMethodId, DateTime paymentDate)
        {
            _logger.LogDebug($"Create payment for accountId:{accountId}, billId:{billId}, amount: {amount}, paymentMethod: {paymentMethodId}, date: {paymentDate}");

            var payment = new Payment
            {
                AccountId = accountId,
                BillId = billId,
                Amount = amount,
                PaymentDate = paymentDate,
                PaymenMethodId = paymentMethodId
            };

            // @todo implement idempotency check
            // stupid one, not thread safe, bad performance
            var payments = await _unitOfWork.Payments.GetAllAsync();
            var nextId = payments.Max(x => x.Id) + 1;

            var matchingPayments = payments.Select(x => x.AccountId == accountId
                                                        && x.BillId == billId
                                                        && x.PaymenMethodId == paymentMethodId
                                                        && x.Amount == amount
                                                        && x.PaymentDate.AddHours(1) < DateTime.Today);
            if (matchingPayments.Count() > 0)
            {
                _logger.LogWarning($"Found {matchingPayments.Count()} duplicate payments");
                throw new Exception( "duplicate payments");
            }
            // -----

            payment.Id = nextId;

            return await _unitOfWork.Payments.AddAsync(payment);
        }

        public async Task<IReadOnlyList<Bill>> QueryBillsAsync(long? accountId, long? vendorId, bool? isPaid = false)
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


        /**
        * Crud methods implementatoion for Bill/Vendor/Payment/etc entities should be in separate services
        */
    }
}

