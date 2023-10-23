using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AccountPayable.Core.Repos
{
    public class VendorRepository : BaseRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(ILogger<BaseRepository<Vendor>> logger, IConfiguration configuration) : base(logger, configuration)
        {
        }
    }
}

