using System.Threading.Tasks;
using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.ModuleOfLog.DTOs;
using Newtonsoft.Json;
using RestSharp;

namespace LQClass.ModuleOfLog.Models;

public class MainTabItemModel
{
    /// <summary>
    ///     查询日志
    /// </summary>
    /// <returns></returns>
    public async Task<IRestResponse> Search(ActionLogSearcherDto actionLogSearcherDto)
    {
        var searchStr = JsonConvert.SerializeObject(actionLogSearcherDto);

        var client = new RestClient($"{AppSettingsHelper.API}_actionlog/search");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Authorization", $"Bearer {LoginJwtResultDto.Instance.AccessToken}");
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", searchStr, ParameterType.RequestBody);
        var response = await client.ExecuteAsync(request);

        return response;
    }
}