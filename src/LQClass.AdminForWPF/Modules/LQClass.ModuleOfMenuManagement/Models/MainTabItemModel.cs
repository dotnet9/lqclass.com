using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.ModuleOfMenuManagement.DTOs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.ModuleOfMenuManagement.Models
{
    public class MainTabItemModel
    {
        /// <summary>
        ///     查询菜单
        /// </summary>
        /// <returns></returns>
        public async Task<IRestResponse> Search(MenuSearcherDTO actionLogSearcherDto)
        {
            var searchStr = JsonConvert.SerializeObject(actionLogSearcherDto);

            var client = new RestClient($"{AppSettingsHelper.API + Apis.Search}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
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
        public async Task<IRestResponse> MenuIdInfo(string Id)
        {

            var client = new RestClient($"{AppSettingsHelper.API + Apis.MenuId + Id}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {LoginJwtResultDto.Instance.AccessToken}");
            var response = await client.ExecuteAsync(request);

            return response;
        }

    }
}
