namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.UpdateGiftCard;

[Route("api/v1/gift-card")]
[ApiController]
public class UpdateGiftCardEndpoint : BaseController
{
    private readonly IMediator _mediator;

    public UpdateGiftCardEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGiftCard([FromBody] GiftCardRequestDTO giftCardRequest, string id, CancellationToken cs)
    {
        var command = new UpdateGiftCardCommand(id, giftCardRequest);
        var result = await _mediator.Send(command, cs);

        return Content(result);
    }
}
