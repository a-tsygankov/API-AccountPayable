using AccountPayable.Service.Interfaces;
using AccountPayable.Service.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace AccountPayable.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountPayableController : ControllerBase
{
    private readonly IAccountPayableService _service;
    private readonly ILogger<AccountPayableController> _logger;

    public AccountPayableController(IAccountPayableService service, ILogger<AccountPayableController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet(Name = "GetBills")]
    public async Task<IEnumerable<BillRM>> Get(long? accountId, long? vendorId, bool isPaid = false)
    {
        var bills = await _service.QueryBillsAsync(accountId, vendorId, isPaid);

        return Array.Empty<BillRM>();
    }
}

