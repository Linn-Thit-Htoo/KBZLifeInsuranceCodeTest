using KBZLifeInsuranceCodeTest.DTOs.Features.PageSetting;

namespace KBZLifeInsuranceCodeTest.DTOs.Features.Account;

public record AccountListDTO(List<AccountDTO> Accounts, PageSettingDTO PageSetting);
