using LQClass.IRepository;
using LQClass.IService;
using LQClass.Repository;
using LQClass.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class IOCExtend
	{
		public static IServiceCollection AddCustomIOC(this IServiceCollection services)
		{
			services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
			services.AddScoped<IBlogNewsService, BlogNewsService>();

			services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
			services.AddScoped<ITypeInfoService, TypeInfoService>();

			services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
			services.AddScoped<IWriterInfoService, WriterInfoService>();

			return services;
		}
	}
}
