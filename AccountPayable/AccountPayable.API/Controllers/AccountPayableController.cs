using AccountPayable.Service.Interfaces;
using AccountPayable.Service.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace AccountPayable.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountPayableController : ControllerBase
{
    private readonly IAccountPayableService _service;
    private readonly IReadModelService _readModelService;
    private readonly ILogger<AccountPayableController> _logger;

    public AccountPayableController(IAccountPayableService service, IReadModelService readModelService, ILogger<AccountPayableController> logger)
    {
        _service = service;
        _readModelService = readModelService;
        _logger = logger;
    }

    [HttpGet(Name = "GetBills")]
    public async Task<IEnumerable<BillRM>> QueryBills(long? accountId, long? vendorId, bool isPaid = false)
    {
        var bills = await _service.QueryBillsAsync(accountId, vendorId, isPaid);
        var billRMs = await _readModelService.GetBillReadModelAsync((IList<Core.Entities.Bill>)bills);

        return billRMs;
    }
}

