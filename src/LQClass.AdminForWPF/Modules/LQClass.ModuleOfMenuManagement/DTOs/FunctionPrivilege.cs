using System;

namespace LQClass.ModuleOfMenuManagement.DTOs;

public class FunctionPrivilege
{
    public string RoleCode { get; set; }

    public Guid MenuItemId { get; set; }

    public FrameworkMenu MenuItem { get; set; }

    public bool? Allowed { get; set; }
}