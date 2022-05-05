using System.Net;
using System.Threading.Tasks;
using LQClass.AdminForWPF.Infrastructure.Configs;
using Newtonsoft.Json;
using RestSharp;

namespace MyToDo.Service;

public class HttpRestClient<T>
{
    private readonly string apiUrl;
    protected readonly RestClient client;

    public HttpRestClient()
    {
        apiUrl = AppSettingsHelper.API;
        client = new RestClient(apiUrl);
    }

    public async Task<T> ExecuteAsync(BaseRequest baseRequest)
    {
        var request = new RestRequest(baseRequest.Route, baseRequest.Method);
        request.AddHeader("Content-Type", baseRequest.ContentType);

        if (baseRequest.Parameter != null)
            request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter),
                ParameterType.RequestBody);
        var response = await client.ExecuteAsync(request);
        if (response.StatusCode == HttpStatusCode.OK)
            return JsonConvert.DeserializeObject<T>(response.Content);

        return JsonConvert.DeserializeObject<T>("");
    }

    public async Task<T> ExecuteAsync<T>(BaseRequest baseRequest)
    {
        var request = new RestRequest(baseRequest.Route, baseRequest.Method);
        request.AddHeader("Content-Type", baseRequest.ContentType);

        if (baseRequest.Parameter != null)
            request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter),
                ParameterType.RequestBody);
        var response = await client.ExecuteAsync(request);
        if (response.StatusCode == HttpStatusCode.OK)
            return JsonConvert.DeserializeObject<T>(response.Content);

        return JsonConvert.DeserializeObject<T>("");
    }
}