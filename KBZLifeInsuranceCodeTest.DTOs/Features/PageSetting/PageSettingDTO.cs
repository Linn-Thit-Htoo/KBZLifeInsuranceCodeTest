namespace KBZLifeInsuranceCodeTest.DTOs.Features.PageSetting;

public class PageSettingDTO
{
    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public int TotalCount { get; set; }

    public PageSettingDTO(int pageNo, int pageSize, int pageCount, int totalCount)
    {
        PageNo = pageNo;
        PageSize = pageSize;
        PageCount = pageCount;
        TotalCount = totalCount;
    }
}
