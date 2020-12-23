using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfExtensions.Xaml;

namespace LQClass.AdminForWPF.Models
{
	public class MainWindowModel : ViewModelBase
	{
		/// <summary>
		/// ��ȡ���ݲ˵�
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<CustomMenu> ReadCustomMenus()
		{
			var oldMenus = LoginResultDto.Instance.Attributes.Menus;
			ObservableCollection<CustomMenu> customMenus = new ObservableCollection<CustomMenu>();
			ReadChildren(string.Empty, oldMenus, customMenus);
			return customMenus;
		}

		/// <summary>
		/// ��ȡ�Ӳ˵�
		/// </summary>
		/// <param name="parentID"></param>
		/// <param name="customMenus"></param>
		private void ReadChildren(string parentID, List<MenuInfo> soureMenus, ObservableCollection<CustomMenu> customMenus)
		{
			var findResults = soureMenus.ToList().FindAll(cu => cu.ParentId == parentID);
			if (findResults == null || findResults.Count <= 0)
			{
				return;
			}
			foreach (var menuItem in findResults)
			{
				var newMenu = new CustomMenu { ID = menuItem.Id, ParentID = parentID, Name = menuItem.Text };
				customMenus.Add(newMenu);
				ReadChildren(newMenu.ID, soureMenus, newMenu.Children);
			}
		}
	}
}
