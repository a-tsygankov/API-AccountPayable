using System;
namespace AccountPayable.Core.Entities
{
	public class Payment
	{
        public int Id { get; set; }

        public int BillId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public int PaymentId { get; set; }

        public int UpdateCounter { get; set; }
    }
}

