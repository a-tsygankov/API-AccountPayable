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

        /// <summary>
        /// @todo make this a list
        /// If multiple payments then last payment date should appear
        /// </summary>
        public string? PaymentMethodName { get; set; }
        /// <summary>
        /// @todo make this a list
        /// If no payments exist for this bill then null
        /// </summary>
        public decimal? PaidAmount { get; set; }
        /// <summary>
        /// @todo make this a list
        /// Until that If multiple payments then last payment date should appear
        /// </summary>
        public DateTime? PaymentDate { get; set; }  ///

    }
}

