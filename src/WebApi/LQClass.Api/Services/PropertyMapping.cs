using System.Collections.Generic;

namespace LQClass.Api.Services
{
	public class PropertyMapping<TSource, TDestination> : IPropertyMapping
	{
		public Dictionary<string, PropertyMappingValue> mappingDictionary { get; set; }
		public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
		{
			this.mappingDictionary = mappingDictionary;
		}
	}
}
