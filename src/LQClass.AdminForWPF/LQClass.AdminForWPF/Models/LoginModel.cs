using LQClass.AdminForWPF.Infrastructure.Configs;
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
			request.AlwaysMultipartFormData = true;
			request.AddParameter("account", userName);
			request.AddParameter("password", pwd);
			request.AddParameter("rememberLogin", $"{cookie}");
			var response = await client.ExecuteAsync(request);

			return response;
		}
	}
}
