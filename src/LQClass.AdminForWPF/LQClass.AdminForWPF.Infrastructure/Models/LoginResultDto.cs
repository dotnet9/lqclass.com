using System;
using System.Collections.Generic;

namespace LQClass.AdminForWPF.Infrastructure.Models
{
	public class LoginResultDto
	{
		public static LoginResultDto Instance { get; set; }
		public string Id { get; set; }
		public string ITCode { get; set; }
		public string Name { get; set; }
		public string Memo { get; set; }
		public string PhotoId { get; set; }
		public List<RoleDto> Roles { get; set; }
		public object Groups { get; set; }
		public AttributeInfo Attributes { get; set; }
		public object FunctionPrivileges { get; set; }
		public object DataPrivileges { get; set; }
	}

	public class RoleDto
	{
		public string RoleCode { get; set; }
		public string RoleName { get; set; }
		public string RoleRemark { get; set; }
		public int UserCount { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateBy { get; set; }
		public DateTime? UpdateTime { get; set; }
		public string UpdateBy { get; set; }
		public string ID { get; set; }
	}

	public class AttributeInfo
	{
		public List<MenuInfo> Menus { get; set; }
		public List<string> Actions { get; set; }
	}

	public class MenuInfo
	{
		public string Id { get; set; }
		public string ParentId { get; set; }
		private string _Text;
		public string Text
		{
			get { return _Text; }
			set
			{
				_Text = value.Replace(" ", "_");
			}
		}
		public string Url { get; set; }
		public string Icon { get; set; }
	}
}
