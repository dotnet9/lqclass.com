using LQClass.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace LQClass.Api.Helper
{
	public static class IQueryableExtensions
	{
		public static IQueryable<T> ApplySort<T>(
			this IQueryable<T> source,
			string orderBy,
			Dictionary<string, PropertyMappingValue> mappingDictionary
			)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			if (mappingDictionary == null)
			{
				throw new ArgumentNullException("mappingDictionary");
			}

			if (string.IsNullOrWhiteSpace(orderBy))
			{
				return source;
			}

			var orderByString = string.Empty;

			var orderByAfterSplit = orderBy.Split(',');

			foreach (var order in orderByAfterSplit)
			{
				var trimmedOrder = order.Trim();

				// 通过字符串" desc"来判断升序还是降序
				var orderDescending = trimmedOrder.EndsWith(" desc");

				// 通过字符串或降序字符串 " asc" or " desc"来获得属性的名称
				var indexOfFirstSpace = trimmedOrder.IndexOf(" ");
				var propertyName = (indexOfFirstSpace == -1)
					? trimmedOrder
					: trimmedOrder.Remove(indexOfFirstSpace);

				if (!mappingDictionary.ContainsKey(propertyName))
				{
					throw new ArgumentException($"Key mapping for {propertyName}");
				}

				var propertyMappingValue = mappingDictionary[propertyName];
				if (propertyMappingValue == null)
				{
					throw new ArgumentNullException("propertyMappingValue");
				}

				foreach (var destinationProperty in
					propertyMappingValue.DestinationProperties.Reverse())
				{
					// 给IQueryable 添加排序字符串
					orderByString = orderByString +
						(string.IsNullOrWhiteSpace(orderByString) ? string.Empty : ", ")
						+ destinationProperty
						+ (orderDescending ? " descending" : " ascending");
				}
			}

			return source.OrderBy(orderByString);
		}
	}
}
