using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using AccountPayable.Service.Tests.Mocks;

namespace AccountPayable.Service.Tests;

public class AccountPayableServiceTests
{
    IVendorRepository vendors;
    public AccountPayableServiceTests()
    {
        // run fixture

        vendors = Fixture.createVendorRepo();
    }

    [Fact]
    public void GetAllBillsTest()
    {
        Assert.NotNull(vendors);
    }
}
