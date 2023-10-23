using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using AccountPayable.Service.Interfaces;
using AccountPayable.Service.ReadModels;
using Microsoft.Extensions.Logging;

namespace AccountPayable.Service.Services
{
    public class ReadModelService : IReadModelService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReadModelService> _logger;

        public ReadModelService(IUnitOfWork unitOfWork,
                                ILogger<ReadModelService> logger)
		{
            _unitOfWork = unitOfWork;
            _logger = logger;

            _logger.LogInformation("Service created");

        }


        public async Task <IReadOnlyList<BillRM>> GetBillReadModelAsync(IList<Bill> bills)
        {
            _logger.LogDebug($"Building Bill read models, count: {bills.Count}");

            var result = bills.Select(async bill =>
            {
                var paymentsForBill = await _unitOfWork.Payments.GetByBillIdAsync(bill.Id);
                return new BillRM()
                {
                    Id = bill.Id,
                    AccountId = bill.AccountId,
                    OrderOf = bill.OrderOf,
                    Amount = bill.Amount,
                    DueDate = bill.DueDate,
                    Paid = bill.Paid,
                    PaymentMethodName = null,
                };
            }).ToList();

            return (IReadOnlyList<BillRM>)result;
        }

        public async Task<IReadOnlyList<PaymentRM>> GetPaymentReadModelAsync(IList<Payment> payments)
        {
            var result = await payments.ToAsyncEnumerable().
                SelectAwait(
                async payment =>
                {
                    var bill = await _unitOfWork.Bills.GetByIdAsync(payment.BillId);
                    var method = await _unitOfWork.PaymentMethods.GetByIdAsync(payment.PaymenMethodId);
                    var vendor = await _unitOfWork.Vendors.GetByIdAsync(bill.VendorId);

                    var paymentRM = new PaymentRM()
                    {
                        Id = payment.Id,
                        AccountId = payment.AccountId,
                        BillId = payment.BillId,
                        Amount = payment.Amount,
                        PaymentDate = payment.PaymentDate,
                        OrderOf = bill.OrderOf,
                        VendorName = vendor.DisplayName,
                        PaymentMethodName = method.DisplayName
                    };

                    return paymentRM;
                }).ToListAsync();

            return await Task.FromResult(result);
        }
    }
}

