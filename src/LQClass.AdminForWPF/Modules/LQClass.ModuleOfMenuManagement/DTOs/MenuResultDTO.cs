using System.Collections.Generic;

namespace LQClass.ModuleOfMenuManagement.DTOs;

public class MenuResultDTO
{
    public List<MenuDTO> Data;

    public int Count { get; set; }

    public int Page { get; set; }

    public int PageCount { get; set; }

    public string Msg { get; set; }

    public int Code { get; set; }
}