using PaymentService.Application.Models;
using PaymentService.Dal.Entities;

namespace PaymentService.Extensions.ModelConversions;

public static class PaymentConversionExtensions
{
	public static Payment ToPayment(this AddPaymentRequest request) =>
		new()
		{
			Id = Guid.NewGuid(),
			BookingId = request.BookingId,
			Amount = request.Amount,
			Status = Dal.Enums.PaymentStatus.Pending,
			CreatedAt = DateTime.UtcNow
		};
}
