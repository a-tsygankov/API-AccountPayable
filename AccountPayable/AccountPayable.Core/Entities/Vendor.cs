using AccountPayable.Core.Interfaces;

namespace AccountPayable.Core.Entities
{
    public class Vendor : IEntity
	{
        public long Id { get; set; }

        public required string DisplayName { get; set; }

        //public long UpdateCounter { get; set; }
    }
}

