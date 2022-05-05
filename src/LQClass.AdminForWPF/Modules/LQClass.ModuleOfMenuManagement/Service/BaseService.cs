using System.Threading.Tasks;
using RestSharp;

namespace MyToDo.Service;

public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
{
    private readonly string serviceName;

    public BaseService(string serviceName)
    {
        this.serviceName = serviceName;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var client = new HttpRestClient<TEntity>();
        var request = new BaseRequest();
        request.Method = Method.Post;
        request.Route = $"api/{serviceName}/Add";
        request.Parameter = entity;
        return await client.ExecuteAsync<TEntity>(request);
    }

    public async Task<TEntity> DeleteAsync(int id)
    {
        var client = new HttpRestClient<TEntity>();
        var request = new BaseRequest();
        request.Method = Method.Delete;
        request.Route = $"api/{serviceName}/Delete?id={id}";
        return await client.ExecuteAsync(request);
    }

    public async Task<TEntity> GetAllAsync(TEntity parameter)
    {
        var client = new HttpRestClient<TEntity>();
        var request = new BaseRequest();
        request.Method = Method.Get;
        //request.Route = $"api/{serviceName}/GetAll?pageIndex={parameter.PageIndex}" +
        //    $"&pageSize={parameter.PageSize}" +
        //    $"&search={parameter.Search}";
        return await client.ExecuteAsync<TEntity>(request);
    }

    public async Task<TEntity> GetFirstOfDefaultAsync(int id)
    {
        var client = new HttpRestClient<TEntity>();
        var request = new BaseRequest();
        request.Method = Method.Get;
        request.Route = $"api/{serviceName}/Get?id={id}";
        return await client.ExecuteAsync<TEntity>(request);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var client = new HttpRestClient<TEntity>();
        var request = new BaseRequest();
        request.Method = Method.Post;
        request.Route = $"api/{serviceName}/Update";
        request.Parameter = entity;
        return await client.ExecuteAsync<TEntity>(request);
    }
}