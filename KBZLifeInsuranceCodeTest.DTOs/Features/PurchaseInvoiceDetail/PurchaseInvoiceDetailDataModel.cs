namespace KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoiceDetail
{
    public class PurchaseInvoiceDetailDataModel
    {
        public string Id { get; set; }
        public string GiftCardId { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public string TypeOfBuying { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string GiftCardNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public byte[] QRCode { get; set; }
        public int GiftCardDuration { get; set; }
        public int CashbackPercentage { get; set; }
    }
}
