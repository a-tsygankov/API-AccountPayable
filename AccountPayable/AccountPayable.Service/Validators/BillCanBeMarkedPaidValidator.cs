using System;
using AccountPayable.Core.Entities;
using AccountPayable.Core.Interfaces;

namespace AccountPayable.Service.Validators
{
	public class BillCanBeMarkedPaidValidator : EntityValidator<Bill>
	{
		public BillCanBeMarkedPaidValidator(IPaymentRepository paymentRepository) : base(
			entity =>
			{
				// @todo rework to proper async
                var payments = paymentRepository.GetByBillIdAsync(entity.Id).GetAwaiter().GetResult();

				var total = payments.Sum(x => x.Amount);
                Bill? bill = entity as Bill;

				return (payments.Count > 0 && total == bill?.Amount);
					
			})
		{
		}
	}
}

