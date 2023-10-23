using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AccountPayable.Core.Repos
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        public BillRepository(ILogger<BaseRepository<Bill>> logger, IConfiguration configuration) : base(logger, configuration)
        {
        }
    }
}

