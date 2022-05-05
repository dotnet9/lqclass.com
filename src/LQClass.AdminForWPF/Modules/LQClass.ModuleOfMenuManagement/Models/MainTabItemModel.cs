using System.Threading.Tasks;
using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.ModuleOfMenuManagement.DTOs;
using Newtonsoft.Json;
using RestSharp;

namespace LQClass.ModuleOfMenuManagement.Models;

public class MainTabItemModel
{
    /// <summary>
    ///     查询菜单
    /// </summary>
    /// <returns></returns>
    public async Task<RestResponse> Search(MenuSearcherDTO actionLogSearcherDto)
    {
        var searchStr = JsonConvert.SerializeObject(actionLogSearcherDto);

        var client = new RestClient(AppSettingsHelper.API);
        var request = new RestRequest(Apis.Search, Method.Post);
        request.AddHeader("Authorization", $"Bearer {LoginJwtResultDto.Instance.AccessToken}");
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", searchStr, ParameterType.RequestBody);
        var response = await client.ExecuteAsync(request);

        return response;
    }

    /// <summary>
    ///     查询单个信息
    /// </summary>
    /// <returns></returns>
    public async Task<RestResponse> MenuIdInfo(string Id)
    {
        var client = new RestClient($"{AppSettingsHelper.API}");
        var request = new RestRequest(Apis.MenuId + Id, Method.Post);
        request.AddHeader("Authorization", $"Bearer {LoginJwtResultDto.Instance.AccessToken}");
        var response = await client.ExecuteAsync(request);

        return response;
    }

    /// <summary>
    ///     新增菜单
    /// </summary>
    /// <returns></returns>
    public async Task<RestResponse> AddMenu(EntityDTO FrameworkMenuDto)
    {
        var searchStr = JsonConvert.SerializeObject(FrameworkMenuDto);

        var client = new RestClient(AppSettingsHelper.API);
        var request = new RestRequest(Apis.AddMenu, Method.Post);
        request.AddHeader("Authorization", $"Bearer {LoginJwtResultDto.Instance.AccessToken}");
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", searchStr, ParameterType.RequestBody);
        var response = await client.ExecuteAsync(request);
        return response;
    }

    /// <summary>
    ///     获取可当父级目录列表
    /// </summary>
    /// <returns></returns>
    public async Task<RestResponse> GetFolderFunction()
    {
        var client = new RestClient(AppSettingsHelper.API);
        var request = new RestRequest(Apis.GetFolders);
        request.AddHeader("Authorization", $"Bearer {LoginJwtResultDto.Instance.AccessToken}");
        var response = await client.ExecuteAsync(request);
        return response;
    }
}