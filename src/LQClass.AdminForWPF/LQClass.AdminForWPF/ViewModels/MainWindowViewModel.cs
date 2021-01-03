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
		#region ����

		private ObservableCollection<CustomMenuItem> _CustomMenus = new ObservableCollection<CustomMenuItem>();
		/// <summary>
		/// �˵��б�
		/// </summary>
		public ObservableCollection<CustomMenuItem> CustomMenus { get { return _CustomMenus; } }

		private CustomMenuItem _SelectedMenuItem;
		/// <summary>
		/// ѡ��Ĳ˵�
		/// </summary>
		public CustomMenuItem SelectedMenuItem
		{
			get { return _SelectedMenuItem; }
			set { this.SetProperty(ref _SelectedMenuItem, value); }
		}


		#endregion

		#region ����


		private ICommand _RaiseLogoutCommand;
		/// <summary>
		/// �û�����
		/// </summary>
		public ICommand RaiseLogoutCommand =>
		  _RaiseLogoutCommand ?? (_RaiseLogoutCommand = new DelegateCommand(RaiseLogoutHandler));

		private ICommand _RaiseSelectedItemCommand;
		/// <summary>
		/// ѡ��˵�����
		/// </summary>
		public ICommand RaiseSelectedItemCommand =>
		  _RaiseSelectedItemCommand ?? (_RaiseSelectedItemCommand = new DelegateCommand<CustomMenuItem>(RaiseSelectedItemHandler));

		#endregion

		#region ˽�б���

		private MainWindowModel windowModel = null;

		#endregion

		public MainWindowViewModel(MainWindowModel mainWindowModel, IRegionManager regionManager) : base(regionManager)
		{
			windowModel = mainWindowModel;
		}

		/// <summary>
		/// ��ʼ��
		/// </summary>
		public void InitData()
		{
			_CustomMenus.Clear();
			_CustomMenus.AddRange(windowModel.ReadCustomMenus());
		}

		public bool NeedShowHome { get; set; } = true;
		/// <summary>
		/// ��ʾ��ҳ
		/// </summary>
		public void ShowHome()
		{
			if (NeedShowHome)
			{
				SelectedMenuItem = _CustomMenus.First(cu => cu.Key == CustomMenuItem.KEY_OF_HOME);
				RaiseSelectedItemHandler(SelectedMenuItem);
			}
		}

		#region �������

		/// <summary>
		/// ע������
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
		/// ѡ��˵�����
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
