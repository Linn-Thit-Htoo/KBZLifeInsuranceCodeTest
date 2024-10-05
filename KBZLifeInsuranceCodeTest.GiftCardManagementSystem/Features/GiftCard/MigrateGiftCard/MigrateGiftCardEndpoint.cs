using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.MigrateGiftCard
{
    [Route("api/v1/gift-card")]
    [ApiController]
    public class MigrateGiftCardEndpoint : BaseController
    {
        private readonly IMediator _mediator;

        public MigrateGiftCardEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("migrate")]
        public async Task<IActionResult> MigrateGiftCard(CancellationToken cs)
        {
            var command = new MigrateGiftCardCommand();
            var result = await _mediator.Send(command, cs);

            return Content(result);
        }
    }
}
