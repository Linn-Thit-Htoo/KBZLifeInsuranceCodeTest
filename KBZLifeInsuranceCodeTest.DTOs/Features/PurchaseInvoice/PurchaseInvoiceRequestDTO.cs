using KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoiceDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice
{
    public class PurchaseInvoiceRequestDTO
    {
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public List<PurchaseInvoiceDetailRequestDTO> PurchaseInvoiceDetailRequests { get; set; }
    }
}
