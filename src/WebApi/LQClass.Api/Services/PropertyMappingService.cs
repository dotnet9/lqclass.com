using LQClass.Api.Dtos;
using LQClass.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LQClass.Api.Services
{
	public class PropertyMappingService : IPropertyMappingService
	{
		private Dictionary<string, PropertyMappingValue> touristRoutePropertyMapping =
			new Dictionary<string, PropertyMappingValue>
			{
				{ "Id", new PropertyMappingValue(new List<string>(){ "Id"})},
				{ "Title", new PropertyMappingValue(new List<string>(){ "Title"})},
				{ "Rating", new PropertyMappingValue(new List<string>(){ "Rating"})},
				{ "OriginalPrice", new PropertyMappingValue(new List<string>(){ "OriginalPrice"})},
			};

		private IList<IPropertyMapping> propertyMappings = new List<IPropertyMapping>();

		public PropertyMappingService()
		{
			propertyMappings.Add(
				new PropertyMapping<TouristRouteDto, TouristRoute>(
					touristRoutePropertyMapping));
		}

		public Dictionary<string, PropertyMappingValue>
			GetPropertyMapping<TSource, TDestination>()
		{
			// 获得匹配的映射对象
			var matchingMapping =
				propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

			if (matchingMapping.Count() == 1)
			{
				return matchingMapping.First().mappingDictionary;
			}

			throw new Exception(
				$"Cannot find exact property mapping instance for <{typeof(TSource)}, {typeof(TDestination)}>");
		}

		public bool IsMappingExists<TSource, TDestination>(string fields)
		{
			var propertyMapping = GetPropertyMapping<TSource, TDestination>();

			if (string.IsNullOrWhiteSpace(fields))
			{
				return true;
			}

			// 逗号来分隔字段字符串
			var fieldsAfterSplit = fields.Split(",");

			foreach (var field in fieldsAfterSplit)
			{
				//去掉空格
				var trimmedField = field.Trim();

				// 获得属性名称字符串
				var indexOfFirstSpace = trimmedField.IndexOf(" ");
				var propertyName = (indexOfFirstSpace == -1) ?
					trimmedField : trimmedField.Remove(indexOfFirstSpace);

				if (!propertyMapping.ContainsKey(propertyName))
				{
					return false;
				}
			}

			return true;
		}

		public bool IsPropertiesExists<T>(string fields)
		{
			if (string.IsNullOrWhiteSpace(fields))
			{
				return true;
			}

			// 逗号来分隔字符串
			var fieldsAfterSplit = fields.Split(',');

			foreach (var field in fieldsAfterSplit)
			{
				// 获得属性名称字符串
				var propertyName = fields.Trim();

				var propertyInfo = typeof(T)
					.GetProperty(
						propertyName,
						BindingFlags.IgnoreCase | BindingFlags.Public
							| BindingFlags.Instance
					);

				// 如果T中没找到对应的属性
				if(propertyInfo==null)
				{
					return false;
				}
			}

			return true;
		}
	}
}
