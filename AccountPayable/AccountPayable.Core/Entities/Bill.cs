using System;
namespace AccountPayable.Core.Entities
{
	public class Bill
	{
        public long Id { get; set; }

        public long AccountId { get; set; }

        public long VendorId { get; set; }

        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public bool Paid { get; set; }

        public long UpdateCounter { get; set; }
    }
}

