using PaymentService.Dal.Enums;

namespace PaymentService.Application.Models;

public class AddPaymentRequest
{
	public Guid BookingId { get; set; }
	public decimal Amount { get; set; }
}
