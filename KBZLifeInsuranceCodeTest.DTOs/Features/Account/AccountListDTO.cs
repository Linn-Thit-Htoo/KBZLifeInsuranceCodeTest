using KBZLifeInsuranceCodeTest.DTOs.Features.PageSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.DTOs.Features.Account
{
    public record AccountListDTO(List<AccountDTO> Accounts, PageSettingDTO PageSetting)
}
