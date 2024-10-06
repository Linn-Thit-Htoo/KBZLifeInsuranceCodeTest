namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.PurchaseInvoice.Purchase
{
    [Route("api/v1/purchase")]
    [ApiController]
    public class PurchaseEndpoint : BaseController
    {
        private readonly IMediator _mediator;

        public PurchaseEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment([FromForm] PurchaseInvoiceRequestDTO purchaseInvoiceRequest, CancellationToken cs)
        {
            var command = new PurchaseCommand(purchaseInvoiceRequest);
            var result = await _mediator.Send(command, cs);

            return Content(result);
        }
    }
}
