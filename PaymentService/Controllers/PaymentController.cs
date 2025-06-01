using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Commands;
using System.Net;

namespace PaymentService.Controllers;

[Route("api/payment")]
[ProducesResponseType(StatusCodes.Status200OK)]
public class PaymentController : ControllerBase
{
	private readonly IMediator _mediator;

	public PaymentController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	public async Task<IActionResult> AddPayment(Guid bookingId, CancellationToken cancellationToken)
	{
		await _mediator.Send(new PayOff.Command(bookingId), cancellationToken);
		return StatusCode((int)HttpStatusCode.NoContent);
	}
}
