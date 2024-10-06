namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.PurchaseInvoice.FilterPurchaseInvoiceListByUser;

[Route("api/v1/purchase-history")]
[ApiController]
public class FilterPurchaseInvoiceListByUserEndpoint : BaseController
{
    private readonly IMediator _mediator;

    public FilterPurchaseInvoiceListByUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FilterPurchaseHistory(string userId, string cardStatus, CancellationToken cs)
    {
        var query = new FilterPurchaseInvoiceListByUserQuery(userId, cardStatus);
        var result = await _mediator.Send(query, cs);

        return Content(result);
    }
}
