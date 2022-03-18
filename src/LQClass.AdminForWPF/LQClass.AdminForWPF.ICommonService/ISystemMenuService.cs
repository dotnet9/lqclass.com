using System.Threading.Tasks;
using LQClass.AdminForWPF.ICommonService.Dtos;

namespace LQClass.AdminForWPF.ICommonService;

public interface ISystemMenuService
{
    Task<SearchMenuResultDto> Search(string apiAddress, string jwtToken, SearchParamDto searchParamDto);
}