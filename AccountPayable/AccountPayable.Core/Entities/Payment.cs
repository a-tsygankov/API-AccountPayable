using System;
namespace AccountPayable.Core.Entities
{
	public class Payment
	{
        public long Id { get; set; }

        public long BillId { get; set; }

        public long AccountId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public long PaymentId { get; set; }

        public long UpdateCounter { get; set; }
    }
}

