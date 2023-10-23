using System;
using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using AccountPayable.Service.Tests.Mocks;
using AccountPayable.Service.Validators;
using Xunit.Abstractions;

namespace AccountPayable.Service.Tests
{
	public class BillCanBeMarkedPaidValidatorTests
	{
        private readonly ITestOutputHelper testOutputHelper;
        IPaymentRepository payments;
        IBillRepository bills;
        public BillCanBeMarkedPaidValidatorTests(ITestOutputHelper testOutputHelper)
		{
            payments = Fixture.createPaymentRepo();
            bills = Fixture.createBillRepo();
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async ValueTask CanValidateNonEmptyList_ValidBill()
        {

            BillCanBeMarkedPaidValidator billCanBeMarkedPaidValidator = new BillCanBeMarkedPaidValidator(payments);
            var allBills = await bills.GetAllAsync();

            Assert.True(billCanBeMarkedPaidValidator.IsValid(allBills.ToArray()[0]));

        }

        [Fact]
        public async ValueTask CanValidateNonEmptyList_UnpaidBill()
        {
            BillCanBeMarkedPaidValidator billCanBeMarkedPaidValidator = new BillCanBeMarkedPaidValidator(payments);
            var allBills = await bills.GetAllAsync();
            var unpaidBill = allBills.FirstOrDefault(bill => bill.Id == 3);

            Assert.NotNull(unpaidBill);
            Assert.False(billCanBeMarkedPaidValidator.IsValid(unpaidBill));

        }
        [Fact]
        public void CanValidateNonEmptyList_NonExistingBill()
        {
            var billCanBeMarkedPaidValidator = new BillCanBeMarkedPaidValidator(payments);
            var nonExisting = new Bill()
            {
                Id = 999,
                AccountId = 1,
                VendorId = 2,
                OrderOf = "Something meaningful",
                Amount = 100,
                DueDate = DateTime.Today.AddDays(7),
                Paid = false,
            };
            Assert.False(billCanBeMarkedPaidValidator.IsValid(nonExisting));

        }

        [Fact]
        public void CanValidateEmptyList_EmptyRepo()
        {
            var repo = new MockPaymentRepo();
            var billCanBeMarkedPaidValidator = new BillCanBeMarkedPaidValidator(repo);

            var fakeBill = new Bill()
            {
                Id = 999,
                AccountId = 1,
                VendorId = 2,
                OrderOf = "Something meaningful",
                Amount = 100,
                DueDate = DateTime.Today.AddDays(7),
                Paid = false,
            };
            Assert.False(billCanBeMarkedPaidValidator.IsValid(fakeBill));
        }

    }
}

