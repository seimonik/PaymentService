using MediatR;
using PaymentService.Application.Models;
using PaymentService.Dal;
using PaymentService.Dal.Entities;
using PaymentService.Extensions.ModelConversions;

namespace PaymentService.Application.Commands;

public static class AddPayment
{
	public record Command(AddPaymentRequest Request) : IRequest<Payment>;

	internal class Handler : IRequestHandler<Command, Payment>
	{
		public PaymentServiceDbContext _dbContext;

		public Handler(PaymentServiceDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Payment> Handle(Command request, CancellationToken cancellationToken)
		{
			var payment = request.Request.ToPayment();
			_dbContext.Payments.Add(payment);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return payment;
		}
	}
}
