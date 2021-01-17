using LQClass.AdminForWPF.Infrastructure;
using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Events;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using LQClass.AdminForWPF.Models;
using LQClass.AdminForWPF.Views;
using LQClass.CustomControls.TabControlHelper;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using WpfExtensions.Xaml;

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

		private ICommand _RaiseSelectedTabItemCommand;
		/// <summary>
		/// 选择TabItem菜单命令
		/// </summary>
		public ICommand RaiseSelectedTabItemCommand =>
		  _RaiseSelectedTabItemCommand ?? (_RaiseSelectedTabItemCommand = new DelegateCommand<object>(RaiseSelectedTabItemHandler));

		#endregion

		#region 私有变量

		private MainWindowModel windowModel = null;

		#endregion

		public MainWindowViewModel(MainWindowModel mainWindowModel,
			IRegionManager regionManager,
			IModuleManager moduleManager,
			IDialogService dialogService,
			IEventAggregator eventAggregator) : base(regionManager, moduleManager, dialogService, eventAggregator)
		{
			windowModel = mainWindowModel;
			eventAggregator.GetEvent<ChangeLanguageSentEvent>().Subscribe(ReceivedChangeLanguage);
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


		public new void CurrentUICultureChanged(CurrentUICultureChangedEventArgs args)
		{

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

		/// <summary>
		/// 显示的菜单TabItem
		/// </summary>
		private Dictionary<string, object> dictExistTabItems = new Dictionary<string, object>();
		/// <summary>
		/// 点击过的菜单项
		/// </summary>
		private Dictionary<string, CustomMenuItem> dictExistMenuItems = new Dictionary<string, CustomMenuItem>();
		/// <summary>
		/// 用于控制左侧的TreeItem与右侧的TabItem选择，同时触发一个
		/// </summary>
		private bool isSelectedItem = false;
		/// <summary>
		/// 选择菜单命令
		/// </summary>
		private void RaiseSelectedItemHandler(CustomMenuItem menuItem)
		{
			try
			{
				if (isSelectedItem)
				{
					return;
				}
				isSelectedItem = true;
				if (menuItem == null
					|| !RegionManager.Regions.ContainsRegionWithName(RegionNames.MainTabRegion))
				{
					return;
				}
				dictExistMenuItems[menuItem.Key] = menuItem;
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
						view.Closer.Icon = $"./../Images/{view.Closer.Key}OfTab.png";
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
			finally
			{
				isSelectedItem = false;
			}
		}

		/// <summary>
		/// 选择TabItem时，同步更新左侧TreeItem选择状态
		/// </summary>
		/// <param name="obj"></param>
		private void RaiseSelectedTabItemHandler(object obj)
		{
			try
			{
				if (isSelectedItem)
				{
					return;
				}
				isSelectedItem = true;
				ICloseable view = obj as ICloseable;
				if (view == null || !dictExistMenuItems.ContainsKey(view.Closer.Key))
				{
					return;
				}
				dictExistMenuItems[view.Closer.Key].IsSelected = true;
			}
			finally
			{
				isSelectedItem = false;
			}
		}

		private void ReceivedChangeLanguage()
		{
			InitData();
		}

		#endregion
	}
}
