using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfExtensions.Xaml;

namespace LQClass.AdminForWPF.Models
{
	public class MainWindowModel : ViewModelBase
	{
		/// <summary>
		/// 获取数据菜单
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<CustomMenu> ReadCustomMenus()
		{
			var oldMenus = LoginResultDto.Instance.Attributes.Menus;
			ObservableCollection<CustomMenu> customMenus = new ObservableCollection<CustomMenu>();

			// 添加首页菜单
			var mainMenu = AppConfig.Instance.GetMenu(CustomMenu.KEY_OF_HOME);
			var newMenu = new CustomMenu(1, "1", "", mainMenu.Value, $"{AppDomain.CurrentDomain.BaseDirectory}Images\\{mainMenu.Icon}");
			customMenus.Add(newMenu);

			ReadChildren(1, string.Empty, oldMenus, customMenus);
			return customMenus;
		}

		/// <summary>
		/// 读取子菜单
		/// </summary>
		/// <param name="parentID"></param>
		/// <param name="customMenus"></param>
		private void ReadChildren(int level, string parentID, List<MenuInfo> soureMenus, ObservableCollection<CustomMenu> customMenus)
		{
			var findResults = soureMenus.ToList().FindAll(cu => cu.ParentId == parentID);
			if (findResults == null || findResults.Count <= 0)
			{
				return;
			}
			foreach (var menuItem in findResults)
			{
				var mainMenu = AppConfig.Instance.GetMenu(menuItem.Text);
				var newMenu = new CustomMenu(level, menuItem.Id, parentID, mainMenu.Value, $"{AppDomain.CurrentDomain.BaseDirectory}Images\\{mainMenu.Icon}");
				customMenus.Add(newMenu);
				ReadChildren(level + 1, newMenu.ID, soureMenus, newMenu.Children);
			}
		}
	}
}
