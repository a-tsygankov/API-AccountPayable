using System;
namespace AccountPayable.Core.Entities
{
	public class PaymentMethod
	{
        public long Id { get; set; }

        public required string DisplayName { get; set; }

        public long UpdateCounter { get; set; }
    }
}

