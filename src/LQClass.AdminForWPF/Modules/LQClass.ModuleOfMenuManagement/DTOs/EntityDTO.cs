using System.Collections.Generic;

namespace LQClass.ModuleOfMenuManagement.DTOs;

public class EntityDTO
{
    public FrameworkMenu Entity { get; set; }
    public string SelectedModule { get; set; }
    public List<string> SelectedActionIDs { get; set; }
    public List<string> SelectedRolesCodes { get; set; }
}