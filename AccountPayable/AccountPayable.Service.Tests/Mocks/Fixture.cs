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
				vendors ?? new Vendor[] {
				new Vendor(){Id = 1, DisplayName = "Ferrari"}
				});
			return repo;
		}
	}
}

