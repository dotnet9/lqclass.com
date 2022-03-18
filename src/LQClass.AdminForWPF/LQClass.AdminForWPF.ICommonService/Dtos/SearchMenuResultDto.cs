using System.Collections.Generic;

namespace LQClass.AdminForWPF.ICommonService.Dtos;

public class SearchMenuResultDto
{
    public int Code { get; set; }
    public string Msg { get; set; }
    public int Page { get; set; }
    public int PageCount { get; set; }
    public int Count { get; set; }
    public List<FrameworkMenu> Data { get; set; }
}

public class FrameworkMenu
{
    public string PageName { get; set; }
    public string ModuleName { get; set; }
    public string ShowOnMenu { get; set; }

    public string FolderOnly { get; set; }

    public string IsPublic { get; set; }

    public string DisplayOrder { get; set; }

    public string Icon { get; set; }
    public string ParentID { get; set; }
    public string TempIsSelected { get; set; }

    public string ID { get; set; }
    public string LAY_CHECKED { get; set; }
}