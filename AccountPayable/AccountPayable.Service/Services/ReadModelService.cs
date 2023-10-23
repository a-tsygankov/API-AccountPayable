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

            var result = bills.Select(bill =>
            {
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
            });
            var result = await _unitOfWor 
        }
	}
}

