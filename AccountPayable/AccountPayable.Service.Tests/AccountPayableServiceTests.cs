using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using AccountPayable.Service.Tests.Mocks;

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
}
