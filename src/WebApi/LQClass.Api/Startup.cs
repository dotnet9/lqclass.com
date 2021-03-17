using LQClass.Api.Database;
using LQClass.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers(setupAction =>
      {
        setupAction.ReturnHttpNotAcceptable = true;
      })
        .AddNewtonsoftJson(setupAction =>
        {
          setupAction.SerializerSettings.ContractResolver =
          new CamelCasePropertyNamesContractResolver();
        })  // 使用jsonpatch需要配置
      .AddXmlDataContractSerializerFormatters() // 支持内容协商，Accept:application/xml|application/json
      .ConfigureApiBehaviorOptions(setupAction =>
      {
        setupAction.InvalidModelStateResponseFactory = context =>
        {
          var problemDetail = new ValidationProblemDetails(context.ModelState)
          {
            Type = "无所谓",
            Title = "数据验证失败",
            Status = StatusCodes.Status422UnprocessableEntity,
            Detail = "请看详细说明",
            Instance = context.HttpContext.Request.Path
          };
          problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
          return new UnprocessableEntityObjectResult(problemDetail)
          {
            ContentTypes = { "application/problem+json" }
          };
        };  // 自定义验证失败返回码：422
      });

      //services.AddTransient<ITouristRouteRepository, MockTouristRouteRepository>();
      services.AddTransient<ITouristRouteRepository, TouristRouteRepository>(); // 注册仓储服务

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "LQClass.Api", Version = "v1" });
      });
      services.AddDbContext<AppDbContext>(option =>
      {
        option.UseSqlite(Configuration["DbContext:ConnectionString"]);
      }); // 注册数据库

      // 扫描profile文件，AutoMapper映射
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LQClass.Api v1"));
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
