using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.CreateAccount
{
    [Route("api/account")]
    [ApiController]
    public class CreateAccountEndpoint : BaseController
    {
        private readonly IMediator _mediator;

        public CreateAccountEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequestDTO accountRequest, CancellationToken cs)
        {
            var command = new CreateAccountCommand(accountRequest);
            var result = await _mediator.Send(command, cs);

            return Content(result);
        }
    }
}
