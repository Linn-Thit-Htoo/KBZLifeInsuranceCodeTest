namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.CreateAccount;

[Route("api/v1/account")]
[ApiController]
public class CreateAccountEndpoint : BaseController
{
    private readonly IMediator _mediator;

    public CreateAccountEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateAccount(
        [FromBody] AccountRequestDTO accountRequest,
        CancellationToken cs
    )
    {
        var command = new CreateAccountCommand(accountRequest);
        var result = await _mediator.Send(command, cs);

        return Content(result);
    }
}
