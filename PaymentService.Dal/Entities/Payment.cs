using PaymentService.Dal.Enums;
using PaymentService.Dal.Interfaces;

namespace PaymentService.Dal.Entities;

public class Payment : IEntity
{
	public Guid Id { get; set; }
	public Guid BookingId { get; set; }
	public decimal Amount { get; set; }
	public PaymentStatus Status { get; set; }
	public DateTime CreatedAt { get; set; }
}
