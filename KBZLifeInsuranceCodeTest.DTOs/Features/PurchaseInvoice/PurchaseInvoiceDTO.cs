using KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoiceDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice
{
    public class PurchaseInvoiceDTO
    {
        public string PurchaseInvoiceId { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string InvoiceNo { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public DateTime CreatedDate { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public List<PurchaseInvoiceDetailDTO> PurchaseInvoiceDetails { get; set; }
    }
}
