namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.DeactivateGiftCard;

[Route("api/v1/gift-card")]
[ApiController]
public class DeactivateGiftCardEndpoint : BaseController
{
    private readonly IMediator _mediator;

    public DeactivateGiftCardEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("deactivate/{id}")]
    public async Task<IActionResult> DeactivateGiftCard(string id)
    {
        var command = new DeactivateGiftCardCommand(id);
        var result = await _mediator.Send(command);

        return Content(result);
    }
}
