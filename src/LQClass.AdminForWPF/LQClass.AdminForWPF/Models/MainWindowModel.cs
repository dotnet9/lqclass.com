using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfExtensions.Xaml;

namespace LQClass.AdminForWPF.Models
{
	public class MainWindowModel : ViewModelBase
	{
		public MainWindowModel(IRegionManager regionManager) : base(regionManager)
		{

		}
		/// <summary>
		/// 获取数据菜单
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<CustomMenuItem> ReadCustomMenus()
		{
			var oldMenus = LoginResultDto.Instance.Attributes.Menus;
			ObservableCollection<CustomMenuItem> customMenus = new ObservableCollection<CustomMenuItem>();

			// 添加首页菜单
			var mainMenu = AppConfig.Instance.GetMenu(CustomMenuItem.KEY_OF_HOME);
			var newMenu = new CustomMenuItem(1, "1", "", mainMenu.Key, mainMenu.Value, $"{AppDomain.CurrentDomain.BaseDirectory}Images\\{mainMenu.Icon}");
			customMenus.Add(newMenu);

			ReadChildren(1, string.Empty, oldMenus, customMenus);
			return customMenus;
		}

		/// <summary>
		/// 读取子菜单
		/// </summary>
		/// <param name="parentID"></param>
		/// <param name="customMenus"></param>
		private void ReadChildren(int level, string parentID, List<MenuInfo> soureMenus, ObservableCollection<CustomMenuItem> customMenus)
		{
			var findResults = soureMenus.ToList().FindAll(cu => cu.ParentId == parentID);
			if (findResults == null || findResults.Count <= 0)
			{
				return;
			}
			foreach (var menuItem in findResults)
			{
				var mainMenu = AppConfig.Instance.GetMenu(menuItem.Text);
				var newMenu = new CustomMenuItem(level, menuItem.Id, parentID, mainMenu.Key, mainMenu.Value, $"{AppDomain.CurrentDomain.BaseDirectory}Images\\{mainMenu.Icon}");
				customMenus.Add(newMenu);
				ReadChildren(level + 1, newMenu.ID, soureMenus, newMenu.Children);
			}
		}
	}
}
