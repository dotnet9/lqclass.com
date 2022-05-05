using System.Net;
using System.Threading.Tasks;
using LQClass.AdminForWPF.ICommonService;
using LQClass.AdminForWPF.ICommonService.Dtos;
using Newtonsoft.Json;
using RestSharp;

namespace LQClass.AdminForWPF.CommonService;

public class SystemMenuService : ISystemMenuService
{
    public async Task<SearchMenuResultDto> Search(string apiAddress, string jwtToken, SearchParamDto searchParamDto)
    {
        var client = new RestClient($"{apiAddress}");
        var request = new RestRequest("_FrameworkMenu/Search", Method.Post);
        request.AddHeader("Authorization", $"Bearer {jwtToken}");
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", JsonConvert.SerializeObject(searchParamDto),
            ParameterType.RequestBody);
        var response = await client.ExecuteAsync(request);
        if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            return JsonConvert.DeserializeObject<SearchMenuResultDto>(response.Content);
        return default;
    }
}