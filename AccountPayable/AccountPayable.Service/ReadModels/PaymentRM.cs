namespace AccountPayable.Service.ReadModels
{
    public class PaymentRM
	{
        public long Id { get; set; }

        public long AccountId { get; set; }

        public long BillId { get; set; }

        public string VendorName { get; set; }

        public string OrderOf { get; set; }

        public string PaymentMethodName { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

    }
}

