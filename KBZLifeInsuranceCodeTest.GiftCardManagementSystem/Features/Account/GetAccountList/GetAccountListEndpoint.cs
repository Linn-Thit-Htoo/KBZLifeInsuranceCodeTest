using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.CreateAccount
{
    [Route("api/account")]
    [ApiController]
    public class GetAccountListEndpoint : BaseController
    {
        private readonly IMediator _mediator;

        public GetAccountListEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountList(int pageNo, int pageSize, CancellationToken cs)
        {
            var query = new GetAccountListQuery(pageNo, pageSize);
            var result = await _mediator.Send(query, cs);

            return Content(result);
        }
    }
}
