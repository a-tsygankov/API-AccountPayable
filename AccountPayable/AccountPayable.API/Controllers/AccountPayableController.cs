using AccountPayable.Service.Interfaces;
using AccountPayable.Service.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace AccountPayable.API.Controllers;

[Produces("application/json")]
[ApiController]
[Route("v1/accounts/{accountId}/[action]")]
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

    [ActionName("bills/query")]
    [HttpGet]
    //[ApiConventionMethod()]

    public async Task<IEnumerable<BillRM>> QueryBills(long? accountId, long? vendorId, bool? isPaid = false)
    {
        var bills = await _service.QueryBillsAsync(accountId, vendorId, isPaid);
        var billRMs = await _readModelService.GetBillReadModelAsync((IList<Core.Entities.Bill>)bills);

        return billRMs;
    }

    [ActionName("bills/mark-paid")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> MarkPaid(long? accountId, IReadOnlyList<long> billIds)
    {
        try
        {
            var result = await _service.MarkBillsAsPaidAsync(billIds);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
        
    }

    [ActionName("payments")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Create(long accountId, long billId, decimal amount, long paymentMethodId)
    {
        try
        {
            var payment = await _service.CreatePaymentAsync(accountId, billId, amount, paymentMethodId, DateTime.Today);

            return Ok();
        }
        catch(Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }

}

