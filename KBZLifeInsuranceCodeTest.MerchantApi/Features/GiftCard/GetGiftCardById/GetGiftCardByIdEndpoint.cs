namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.GiftCard.GetGiftCardById
{
    [Route("api/v1/gift-card")]
    [ApiController]
    public class GetGiftCardByIdEndpoint : BaseController
    {
        private readonly IMediator _mediator;

        public GetGiftCardByIdEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGiftCardById(string id, CancellationToken cs)
        {
            var query = new GetGiftCardByIdQuery(id);
            var result = await _mediator.Send(query, cs);

            return Content(result);
        }
    }
}
