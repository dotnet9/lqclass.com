using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using RestSharp;
using System.Threading.Tasks;

namespace LQClass.ModuleOfLog.Models
{
	public class MainTabItemModel
	{
		/// <summary>
		/// 查询日志
		/// </summary>
		/// <returns></returns>
		public async Task<IRestResponse> Search()
		{
			var client = new RestClient($"{AppSettingsHelper.API}_actionlog/search");
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			request.AddHeader("Authorization", $"Bearer {LoginJwtResultDto.Instance.AccessToken}");
			request.AddHeader("Content-Type", "application/json");
			request.AddParameter("application/json", "{\"Page\":1,\"Limit\":50}", ParameterType.RequestBody);
			IRestResponse response = await client.ExecuteAsync(request);

			return response;
		}
	}
}
