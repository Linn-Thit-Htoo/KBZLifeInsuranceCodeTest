using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.Shared
{
    public class CommonQuery
    {
        public static string Sp_FilterPurchaseInvoiceByUserId { get; } = "Sp_FilterPurchaseInvoiceByUserId";
        public static string Sp_GetGiftCardDetailsByInvoiceAndStatus { get; } = "Sp_GetGiftCardDetailsByInvoiceAndStatus";
    }
}
