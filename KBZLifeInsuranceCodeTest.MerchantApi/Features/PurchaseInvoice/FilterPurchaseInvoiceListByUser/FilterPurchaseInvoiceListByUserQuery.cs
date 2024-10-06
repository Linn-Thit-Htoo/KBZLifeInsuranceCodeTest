using KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice;

namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.PurchaseInvoice.FilterPurchaseInvoiceListByUser
{
    public class FilterPurchaseInvoiceListByUserQuery : IRequest<Result<PurchaseInvoiceListDTO>>
    {
        public string UserId { get; set; }
        public string CardStatus { get; set; }

        public FilterPurchaseInvoiceListByUserQuery(string userId, string cardStatus)
        {
            UserId = userId;
            CardStatus = cardStatus;
        }
    }
}
