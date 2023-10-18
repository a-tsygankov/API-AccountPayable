using System;
namespace AccountPayable.Service.ReadModels
{
	public class BillModel
	{
        public long Id { get; set; }

        public required string OrderOf { get; set; }

        public long AccountId { get; set; }

        public string VendorName { get; set; }

        public string MaymentMethodName { get; set; }

        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public bool Paid { get; set; }
    }
}

