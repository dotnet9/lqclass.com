using System.Collections.Generic;

namespace LQClass.ModuleOfLog.DTOs;

public class SearchLogResultDto
{
    public List<LogDto> Data;

    public int Count { get; set; }

    public int Page { get; set; }

    public int PageCount { get; set; }

    public string Msg { get; set; }

    public int Code { get; set; }
}