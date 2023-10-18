using System;
namespace AccountPayable.Core.Entities
{
	public class Bill
	{
        public int Id { get; set; }

        public int AccountId { get; set; }

        public int VendorId { get; set; }

        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public int UpdateCounter { get; set; }
    }
}

