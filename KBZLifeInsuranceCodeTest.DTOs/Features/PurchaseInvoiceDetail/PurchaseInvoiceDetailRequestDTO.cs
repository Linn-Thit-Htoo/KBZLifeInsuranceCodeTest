using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoiceDetail
{
    public class PurchaseInvoiceDetailRequestDTO
    {
        public string GiftCardId { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public string TypeOfBuying { get; set; }
        public string? RecipientName { get; set; }
        public string? RecipientPhoneNumber { get; set; }
    }
}
