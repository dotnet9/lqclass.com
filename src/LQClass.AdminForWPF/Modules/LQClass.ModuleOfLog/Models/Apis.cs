using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.ModuleOfLog.Models
{
	public static class Apis
	{
		public const string Delete = "/_ActionLog/BatchDelete";
		public const string Detail = "/_actionlog/{ID}";
		public const string Export = "/_actionlog/ExportExcel";
	}
}
