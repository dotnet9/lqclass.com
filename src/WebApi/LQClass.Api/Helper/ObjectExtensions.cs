﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LQClass.Api.Helper
{
	public static class ObjectExtensions
	{
		public static ExpandoObject ShapeData<TSource>(
					this TSource source,
					string fields
				)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			var dataShapedObject = new ExpandoObject();

			if (string.IsNullOrWhiteSpace(fields))
			{
				// all public properties should be in the ExpandObject
				var propertyInfos = typeof(TSource)
					.GetProperties(BindingFlags.IgnoreCase
					| BindingFlags.Public | BindingFlags.Instance);

				foreach (var propertyInfo in propertyInfos)
				{
					// get the value of the property on the source object
					var propertyValue = propertyInfo.GetValue(source);

					// add the field to the ExpandObject
					((IDictionary<string, object>)dataShapedObject)
						.Add(propertyInfo.Name, propertyValue);
				}

				return dataShapedObject;
			}

			// the field are separated by ",", so we split it.
			var fieldsAfterSplit = fields.Split(',');

			foreach (var field in fieldsAfterSplit)
			{
				// trim each field, as it might contain leading
				// or trailing spaces. Can't trim the var in foreach,
				// so use another var.
				var propertyName = field.Trim();

				var propertyInfo = typeof(TSource)
					.GetProperty(propertyName, BindingFlags.IgnoreCase
					| BindingFlags.Public | BindingFlags.Instance);

				if (propertyInfo == null)
				{
					throw new Exception($"属性 {propertyName} 找不到"
						+ $" {typeof(TSource)}");
				}


				// get the value of the property on the source object
				var propertyValue = propertyInfo.GetValue(source);

				// add the field to the ExpandObject
				((IDictionary<string, object>)dataShapedObject)
					.Add(propertyInfo.Name, propertyValue);
			}

			return dataShapedObject;
		}
	}
}