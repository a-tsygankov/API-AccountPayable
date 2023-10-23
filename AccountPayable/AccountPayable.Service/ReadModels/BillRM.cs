namespace AccountPayable.Service.ReadModels
{
    public class BillRM
	{
        public long Id { get; set; }

        public required string OrderOf { get; set; }

        public long AccountId { get; set; }

        public string VendorName { get; set; }

        public DateTime DueDate { get; set; }

        public bool Paid { get; set; }

        public decimal Amount { get; set; }

        public PaymentRM[]? Payments { get; set; }

    }
}

