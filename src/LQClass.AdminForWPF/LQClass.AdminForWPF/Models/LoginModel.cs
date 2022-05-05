using System.Threading.Tasks;
using LQClass.AdminForWPF.Infrastructure.Configs;
using Newtonsoft.Json;
using RestSharp;

namespace LQClass.AdminForWPF.Models;

public class LoginModel
{
    /// <summary>
    ///     登录
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="pwd"></param>
    /// <param name="cookiet"></param>
    /// <returns></returns>
    public async Task<RestResponse> Login(string userName, string pwd, bool cookie = true)
    {
        var client = new RestClient($"{AppSettingsHelper.API}");
        var request = new RestRequest("_Account/Login", Method.Post);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Accept-Language", "en-US");
        request.AddParameter("account", userName);
        request.AddParameter("password", pwd);
        request.AddParameter("rememberLogin", $"{cookie}");
        var response = await client.ExecuteAsync(request);

        return response;
    }


    /// <summary>
    ///     登录
    /// </summary>
    /// <param name="loginJwtDto"></param>
    /// <returns></returns>
    public async Task<RestResponse> LoginJwt(LoginJwtDto loginJwtDto)
    {
        var client = new RestClient($"{AppSettingsHelper.API}");
        var request = new RestRequest("_Account/LoginJwt", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", JsonConvert.SerializeObject(loginJwtDto), ParameterType.RequestBody);
        var response = await client.ExecuteAsync(request);

        return response;
    }
}

public class LoginJwtDto
{
    public string Account { get; set; }
    public string Password { get; set; }
}