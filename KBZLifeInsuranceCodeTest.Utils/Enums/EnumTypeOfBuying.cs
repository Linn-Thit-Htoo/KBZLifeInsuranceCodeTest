using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.Utils.Enums
{
    public class EnumTypeOfBuying
    {
        public static string GetSelfServiceTypeOfBuying { get; } = "Self Service";
        public static string GetPresentToOtherTypeOfBuying { get; } = "Present To Other";
        public static List<string> GetTypeOfBuyingList() => new()
        {
            "Self Service",
            "Present To Other"
        };
    }
}
