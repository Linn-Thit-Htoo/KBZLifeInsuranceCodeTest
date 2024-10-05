
using KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice;
using KBZLifeInsuranceCodeTest.Utils;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.PurchaseInvoice.Purchase
{
    public class PurchaseCommandHandler : IRequestHandler<PurchaseCommand, Result<PurchaseInvoiceDTO>>
    {
        private readonly IPurchaseInvoiceRepository _purchaseInvoiceRepository;
        private readonly PurchaseValidator _purchaseValidator;
        private readonly IConfiguration _configuration;

        public PurchaseCommandHandler(IPurchaseInvoiceRepository purchaseInvoiceRepository, PurchaseValidator purchaseValidator, IConfiguration configuration)
        {
            _purchaseInvoiceRepository = purchaseInvoiceRepository;
            _purchaseValidator = purchaseValidator;
            _configuration = configuration;
        }

        public async Task<Result<PurchaseInvoiceDTO>> Handle(PurchaseCommand request, CancellationToken cancellationToken)
        {
            Result<PurchaseInvoiceDTO> result;
            try
            {
                var validationResult = await _purchaseValidator.ValidateAsync(request.PurchaseInvoiceRequest, cancellationToken);
                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ErrorMessage));
                    result = Result<PurchaseInvoiceDTO>.Fail(errors);
                    goto result;
                }

                if (request.PurchaseInvoiceRequest.PurchaseInvoiceDetailRequests.Count <= 0)
                {
                    result = Result<PurchaseInvoiceDTO>.Fail("Invalid.");
                    goto result;
                }

                int maxLimit = Convert.ToInt32(_configuration.GetSection("MaximumTicketLimit").Value!);
                if (request.PurchaseInvoiceRequest.PurchaseInvoiceDetailRequests.Count > maxLimit)
                {
                    result = Result<PurchaseInvoiceDTO>.Fail($"You can only purchase {maxLimit} tickets.");
                    goto result;
                }

                result = await _purchaseInvoiceRepository.MakePaymentAsync(request.PurchaseInvoiceRequest, cancellationToken);
            }
            catch (Exception ex)
            {
                result = Result<PurchaseInvoiceDTO>.Fail(ex);
            }

        result:
            return result;
        }
    }
}
