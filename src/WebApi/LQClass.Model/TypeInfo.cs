using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.Model
{
	public class TypeInfo : BaseId
	{
		[SugarColumn(ColumnDataType = "nvarchar(12)")]
		public string Name { get; set; }
	}
}
