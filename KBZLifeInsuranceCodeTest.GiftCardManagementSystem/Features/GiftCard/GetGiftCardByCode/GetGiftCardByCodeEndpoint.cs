using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.GetGiftCardByCode
{
    [Route("api/v1/gift-card")]
    [ApiController]
    public class GetGiftCardByCodeEndpoint : BaseController
    {
        private readonly IMediator _mediator;

        public GetGiftCardByCodeEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetGiftCardByCode(string code, CancellationToken cs)
        {
            var query = new GetGiftCardByCodeQuery(code);
            var result = await _mediator.Send(query, cs);

            return Content(result);
        }
    }
}
