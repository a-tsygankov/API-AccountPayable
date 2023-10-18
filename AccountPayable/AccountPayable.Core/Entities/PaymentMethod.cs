using System;
namespace AccountPayable.Core.Entities
{
	public class PaymentMethod
	{
        public int Id { get; set; }

        public required string DisplayName { get; set; }

        public int UpdateCounter { get; set; }
    }
}

