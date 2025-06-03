using MediatR;
using PaymentService.Application.Models;
using PaymentService.Dal;
using PaymentService.Helper;
using System.Text.Json;

namespace PaymentService.Application.Commands;

public static class PayOff
{
	public record Command(Guid BookingId) : IRequest<Unit>;

	internal class Handler : IRequestHandler<Command, Unit>
	{
		public PaymentServiceDbContext _dbContext;

		public Handler(PaymentServiceDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
		{
			var payment = _dbContext.Payments.FirstOrDefault(x => x.BookingId == request.BookingId && x.Status == Dal.Enums.PaymentStatus.Pending);
			// TODO: If payment is null
			payment!.Status = Dal.Enums.PaymentStatus.Confirmed;
			await _dbContext.SaveChangesAsync(cancellationToken);
			var updateBookingStatusRequest = new UpdateBookingStatusRequest
			{
				BookingId = request.BookingId,
				Status = UpdateBookingStatusRequest.BookingStatus.Confirmed
			};
			Producer.ProduceMessage(JsonSerializer.Serialize(updateBookingStatusRequest), "UpdateBookingStatusRequest");

			return Unit.Value;
		}
	}
}
