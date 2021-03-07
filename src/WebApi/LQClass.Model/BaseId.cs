using SqlSugar;
using System;

namespace LQClass.Model
{
	public class BaseId
	{
		[SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
		public int Id { get; set; }
	}
}
