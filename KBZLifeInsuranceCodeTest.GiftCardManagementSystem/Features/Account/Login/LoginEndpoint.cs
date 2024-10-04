using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.Login
{
    [Route("api/v1/account")]
    [ApiController]
    public class LoginEndpoint : BaseController
    {
        private readonly IMediator _mediator;

        public LoginEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest, CancellationToken cs)
        {
            var query = new LoginQuery(loginRequest);
            var result = await _mediator.Send(query, cs);

            return Content(result);
        }
    }
}
