using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentService.Application.Models;
using PaymentService.Dal;
using System.Text.Json;

namespace PaymentService.Helper;

public class PaymentProcessor : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;

	public PaymentProcessor(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			using var scope = _serviceProvider.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<PaymentServiceDbContext>();
			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

			var fiveMinutesAgo = DateTime.UtcNow.AddMinutes(-3);
			var payments = await dbContext.Payments
				.Where(m => m.Status == Dal.Enums.PaymentStatus.Pending && m.CreatedAt < fiveMinutesAgo)
				.OrderBy(m => m.CreatedAt)
				.ToListAsync(stoppingToken);

			foreach (var payment in payments)
			{
				try
				{
					payment.Status = Dal.Enums.PaymentStatus.Cancelled;
					await dbContext.SaveChangesAsync(stoppingToken);

					var updateBookingStatusRequest = new UpdateBookingStatusRequest
					{
						BookingId = payment.BookingId,
						Status = UpdateBookingStatusRequest.BookingStatus.Cancelled
					};
					Producer.ProduceMessage(JsonSerializer.Serialize(updateBookingStatusRequest), "UpdateBookingStatusRequest");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Произошла ошибка при попытке отправить сообщение в брокер: {ex.Message}");
				}
			}

			await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
		}
	}
}
