namespace PaymentService.Application.Models;

public class UpdateBookingStatusRequest
{
	public Guid BookingId { get; set; }
	public BookingStatus Status { get; set; }

	public enum BookingStatus
	{
		Pending = 0,
		Confirmed = 1,
		Cancelled = 2
	}
}
