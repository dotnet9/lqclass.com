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
    public async Task<RestResponse> Search(ActionLogSearcherDto actionLogSearcherDto)
    {
        var searchStr = JsonConvert.SerializeObject(actionLogSearcherDto);

        var client = new RestClient($"{AppSettingsHelper.API}");
        var request = new RestRequest("_actionlog/search", Method.Post);
        request.AddHeader("Authorization", $"Bearer {LoginJwtResultDto.Instance.AccessToken}");
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", searchStr, ParameterType.RequestBody);
        var response = await client.ExecuteAsync(request);

        return response;
    }
}