using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using AccountPayable.Service.Services;
using AccountPayable.Service.Tests.Mocks;
using Microsoft.Extensions.Logging;
using Moq;

namespace AccountPayable.Service.Tests;

public class AccountPayableServiceTests
{
    IVendorRepository vendors;
    IPaymentMethodRepository paymentMethods;
    IBillRepository bills;
    IPaymentRepository payments;

    public AccountPayableServiceTests()
    {
        // run fixture

        vendors = Fixture.createVendorRepo();
        paymentMethods = Fixture.createPaymentMethodRepo();
        bills = Fixture.createBillRepo();
        payments = Fixture.createPaymentRepo();

    }

    [Fact]
    public void GetAllBillsTest()
    {
        Assert.NotNull(vendors);
    }

    [Fact]
    public async void CanValidateBillsToBeMarkedPaid_Can()
    {
        var logger = Mock.Of<ILogger<AccountPayableService>>();

        var service = new AccountPayableService(vendors, paymentMethods, payments, bills, logger);

        Assert.NotNull(await service.MarkBillsAsPaidAsync(new long[] { 1, 2 }));
    }
}
