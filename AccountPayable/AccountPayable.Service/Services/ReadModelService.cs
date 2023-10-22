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
        private readonly ILogger<AccountPayableService> _logger;

        public ReadModelService(IUnitOfWork unitOfWork,
                                ILogger<AccountPayableService> logger)
		{
            _unitOfWork = unitOfWork;
            _logger = logger;

            _logger.LogInformation("Service created");

        }


        public IReadOnlyList<BillRM> GetBillReadModel(IList<Bill> bills)
        {
            if (bills == null)
                return Array.Empty<BillRM>();
            
            _logger.LogDebug($"Building Bill read models, count: {bills.Count}");

            throw new NotImplementedException();

        }
	}
}

