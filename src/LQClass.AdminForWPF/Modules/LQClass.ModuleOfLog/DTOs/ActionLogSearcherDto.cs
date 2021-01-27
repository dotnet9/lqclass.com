using System.Collections.Generic;

namespace LQClass.ModuleOfLog.DTOs
{
	public class ActionLogSearcherDto
	{
		public string ITCode { get; set; }

		public string ActionUrl { get; set; }


		public List<ActionLogTypesEnum> LogType { get; set; }


		public List<string> ActionTime { get; set; } = new List<string>();


		public string IP { get; set; }

		public double? Duration { get; set; }

		public int Page { get; set; }

		public int Limit { get; set; }
	}
}
