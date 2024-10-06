namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.GiftCard.GetGiftCardList;

[Route("api/v1/gift-card")]
[ApiController]
public class GetGiftCardListEndpoint : BaseController
{
    private readonly IMediator _mediator;

    public GetGiftCardListEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetGiftCardListAsync(
        int pageNo,
        int pageSize,
        CancellationToken cs
    )
    {
        var query = new GetGiftCardListQuery(pageNo, pageSize);
        var result = await _mediator.Send(query, cs);

        return Content(result);
    }
}
