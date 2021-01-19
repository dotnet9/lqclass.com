using LQClass.AdminForWPF.Infrastructure.Configs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.AdminForWPF.Models
{
	public class LoginModel
	{
		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="pwd"></param>
		/// <param name="cookiet"></param>
		/// <returns></returns>
		public async Task<IRestResponse> Login(string userName, string pwd, bool cookie = true)
		{
			var client = new RestClient($"{AppConfig.Instance.API}_Account/Login");
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddHeader("Accept-Language", "en-US");
			request.AddParameter("account", userName);
			request.AddParameter("password", pwd);
			request.AddParameter("rememberLogin", $"{cookie}");
			var response = await client.ExecuteAsync(request);

			return response;
		}


		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="loginJwtDto"></param>
		/// <returns></returns>
		public async Task<IRestResponse> LoginJwt(LoginJwtDto loginJwtDto)
		{
			var client = new RestClient($"{AppConfig.Instance.API}_Account/LoginJwt");
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "application/json");
			request.AddParameter("application/json", JsonConvert.SerializeObject(loginJwtDto), ParameterType.RequestBody);
			IRestResponse response = await client.ExecuteAsync(request);

			return response;
		}
	}

	public class LoginJwtDto
	{
		public string Account { get; set; }
		public string Password { get; set; }
	}
}
