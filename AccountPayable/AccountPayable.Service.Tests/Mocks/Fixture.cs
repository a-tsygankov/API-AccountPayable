using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;

namespace AccountPayable.Service.Tests.Mocks
{
    public class Fixture
	{
		public Fixture()
		{
		}

        public static IVendorRepository createVendorRepo(Vendor[]? vendors = null)
        {
            var repo = new MockVendorRepo();
            repo.Load(
                vendors ??
                new Vendor[] {
                    new Vendor(){Id = 1, DisplayName = "Ferrari"},
                    new Vendor(){Id = 2, DisplayName = "Apple"}
            });

            return repo;
        }
        public static IPaymentMethodRepository createPaymentMethodRepo(PaymentMethod[]? methods = null)
        {
            var repo = new MockPaymentMethodRepo();
            repo.Load(
                methods ??
                new PaymentMethod[] {
                    new PaymentMethod(){Id = 1, DisplayName = "Credit Card"},
                    new PaymentMethod(){Id = 2, DisplayName = "Swift"},
                    new PaymentMethod(){Id = 3, DisplayName = "Direct Deposit"}
            });

            return repo;
        }
        public static IPaymentRepository createPaymentRepo(Payment[]? payments = null)
        {
            var repo = new MockPaymentRepo();
            repo.Load(
                payments ??
                new Payment[] {
                    new Payment(){Id = 1, AccountId = 1, Amount = 2000, BillId = 1, PaymenMethodId = 3, PaymentDate = DateTime.Today},
                    new Payment(){Id = 2, AccountId = 1, Amount = 100, BillId = 2, PaymenMethodId = 1, PaymentDate = DateTime.Today.AddDays(-3)},
            });

            return repo;
        }
        public static IBillRepository createBillRepo(Bill[]? methods = null)
        {
            var repo = new MockBillRepo();
            repo.Load(
                methods ??
                new Bill[] {
                    new Bill(){Id = 1, AccountId = 1, OrderOf = "Car lease", Amount = 2000, DueDate = DateTime.Today.AddDays(1), Paid = false, VendorId = 1 },
                    new Bill(){Id = 2, AccountId = 1, OrderOf = "iPhone 17 pro", Amount = 100, DueDate = DateTime.Today.AddDays(7), Paid = false, VendorId = 2 },
                    new Bill(){Id = 3, AccountId = 2, OrderOf = "Flowers", Amount = 2000, DueDate = DateTime.Today.AddDays(1), Paid = false, VendorId = 1 },
                    new Bill(){Id = 4, AccountId = 1, OrderOf = "Wine", Amount = 2000, DueDate = DateTime.Today.AddDays(1), Paid = false, VendorId = 1 },
            });

            return repo;
        }
    }
}

