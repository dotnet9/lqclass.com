using System.Collections.Generic;

namespace LQClass.Api.Services
{
	public interface IPropertyMappingService
	{
		Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
		bool IsMappingExists<TSource, TDestination>(string fields);
		/// <summary>
		/// 数据塑形参数判断
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fields"></param>
		/// <returns></returns>
		bool IsPropertiesExists<T>(string fields);
	}
}