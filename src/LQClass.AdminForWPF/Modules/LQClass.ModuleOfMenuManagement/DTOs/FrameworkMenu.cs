namespace LQClass.ModuleOfMenuManagement.DTOs;

public class FrameworkMenu
{
    public bool IsInside { get; set; }
    public string Url { get; set; }
    public string PageName { get; set; }
    public string ParentId { get; set; }
    public bool FolderOnly { get; set; }
    public bool ShowOnMenu { get; set; }
    public bool IsPublic { get; set; }
    public int DisplayOrder { get; set; }
    public string Icon { get; set; }
}