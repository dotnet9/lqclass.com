using LQClass.AdminForWPF.ICommonService.Dtos;
using RestSharp;
using System.Threading.Tasks;

namespace LQClass.AdminForWPF.ICommonService
{
	public interface ISystemMenuService
	{
		Task<SearchMenuResultDto> Search(string apiAddress, string jwtToken, SearchParamDto searchParamDto);
	}
}
