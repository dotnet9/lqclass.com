using LQClass.AdminForWPF.Infrastructure;
using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using LQClass.AdminForWPF.Models;
using LQClass.AdminForWPF.Views;
using LQClass.CustomControls.TabControlHelper;
using Prism.Commands;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace LQClass.AdminForWPF.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		#region 属性

		private ObservableCollection<CustomMenuItem> _CustomMenus = new ObservableCollection<CustomMenuItem>();
		/// <summary>
		/// 菜单列表
		/// </summary>
		public ObservableCollection<CustomMenuItem> CustomMenus { get { return _CustomMenus; } }

		private CustomMenuItem _SelectedMenuItem;
		/// <summary>
		/// 选择的菜单
		/// </summary>
		public CustomMenuItem SelectedMenuItem
		{
			get { return _SelectedMenuItem; }
			set { this.SetProperty(ref _SelectedMenuItem, value); }
		}


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
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void InitData()
		{
			_CustomMenus.Clear();
			_CustomMenus.AddRange(windowModel.ReadCustomMenus());
		}

		public bool NeedShowHome { get; set; } = true;
		/// <summary>
		/// 显示首页
		/// </summary>
		public void ShowHome()
		{
			if (NeedShowHome)
			{
				SelectedMenuItem = _CustomMenus.First(cu => cu.Key == CustomMenuItem.KEY_OF_HOME);
				RaiseSelectedItemHandler(SelectedMenuItem);
			}
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

		private Dictionary<string, object> dictExistTabItems = new Dictionary<string, object>();
		/// <summary>
		/// 选择菜单命令
		/// </summary>
		private void RaiseSelectedItemHandler(CustomMenuItem menuItem)
		{
			if (menuItem == null)
			{
				return;
			}
			if (!RegionManager.Regions.ContainsRegionWithName(RegionNames.MainTabRegion))
			{
				return;
			}
			var region = RegionManager.Regions[RegionNames.MainTabRegion];
			if (dictExistTabItems.ContainsKey(menuItem.Key))
			{
				region.Activate(dictExistTabItems[menuItem.Key]);
				return;
			}
			region.RequestNavigate(menuItem.Key);
			dictExistTabItems.Clear();

			foreach (var viewObj in region.Views)
			{
				ICloseable view = viewObj as ICloseable;
				if (view != null)
				{
					dictExistTabItems[view.Closer.Key] = view;
					if (view.Closer.CanClose)
					{
						view.Closer.RequestClose += () =>
						{
							dictExistTabItems.Remove(view.Closer.Key);
							if (region.Views.Contains(view))
							{
								region.Remove(view);
							}
						};
					}
				}
			}
		}

		#endregion
	}
}
