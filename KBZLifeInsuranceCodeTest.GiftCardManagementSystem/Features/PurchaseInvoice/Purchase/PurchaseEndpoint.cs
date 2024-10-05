using KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.PurchaseInvoice.Purchase
{
    [Route("api/purchase")]
    [ApiController]
    public class PurchaseEndpoint : BaseController
    {
        private readonly IMediator _mediator;

        public PurchaseEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment([FromBody] PurchaseInvoiceRequestDTO purchaseInvoiceRequest, CancellationToken cs)
        {
            var command = new PurchaseCommand(purchaseInvoiceRequest);
            var result = await _mediator.Send(command, cs);

            return Content(result);
        }
    }
}
