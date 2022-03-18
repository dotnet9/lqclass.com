namespace LQClass.AdminForWPF.ICommonService.Dtos;

/// <summary>
///     查访条件参数
/// </summary>
public class SearchParamDto
{
    public const int DEFAULT_LIMIT = 50;

    public string SortInfo { get; set; } = null;
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = DEFAULT_LIMIT;
}