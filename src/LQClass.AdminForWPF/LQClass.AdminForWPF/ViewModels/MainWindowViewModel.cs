using HandyControl.Controls;
using LQClass.AdminForWPF.Infrastructure;
using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using LQClass.AdminForWPF.Models;
using LQClass.AdminForWPF.Views;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LQClass.AdminForWPF.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		#region 属性

		private ObservableCollection<CustomMenuItem> _CustomMenus;
		public ObservableCollection<CustomMenuItem> CustomMenus { get { return _CustomMenus; } }

		#endregion

		#region 命令


		private ICommand _RaiseLogoutCommand;
		/// <summary>
		/// 用户命令
		/// </summary>
		public ICommand RaiseLogoutCommand =>
		  _RaiseLogoutCommand ?? (_RaiseLogoutCommand = new DelegateCommand(RaiseLogoutHandler));

		private ICommand _RaiseSelectedItemCommand;
		/// <summary>
		/// 选择菜单命令
		/// </summary>
		public ICommand RaiseSelectedItemCommand =>
		  _RaiseSelectedItemCommand ?? (_RaiseSelectedItemCommand = new DelegateCommand<CustomMenuItem>(RaiseSelectedItemHandler));

		#endregion

		#region 私有变量

		private MainWindowModel windowModel = null;

		#endregion

		public MainWindowViewModel(MainWindowModel mainWindowModel, IRegionManager regionManager) : base(regionManager)
		{
			windowModel = mainWindowModel;
			_CustomMenus = windowModel.ReadCustomMenus();
		}

		#region 命令处理方法

		/// <summary>
		/// 注销命令
		/// </summary>
		/// <param name="key"></param>
		private void RaiseLogoutHandler()
		{
			AppConfig.Instance.IsAutoLogin = false;
			AppConfig.Instance.Save();
			ShellSwitcher.Switch<MainWindowView, LoginView>();
		}
		public event Action<object> AddTabItem;
		/// <summary>
		/// 选择菜单命令
		/// </summary>
		private void RaiseSelectedItemHandler(CustomMenuItem menuItem)
		{
			//if (!RegionManager.Regions.ContainsRegionWithName(RegionNames.MainTabRegion))
			//{
			//	RegionManager.Regions.Add(RegionNames.MainTabRegion, new Region { Name = RegionNames.MainTabRegion });
			//}
			var view = RegionManager.Regions[RegionNames.MainTabRegion].GetView(menuItem.Key);
			AddTabItem?.Invoke(view);
			//RegionManager.Regions[RegionNames.MainTabRegion].RequestNavigate(menuItem.Key) ;
			RegionManager.RequestNavigate(RegionNames.MainTabRegion, "MainTabItemView");
			//TabControl.Items.Add(new TabItem { Header=menuItem.Name});
		}

		#endregion
	}
}
