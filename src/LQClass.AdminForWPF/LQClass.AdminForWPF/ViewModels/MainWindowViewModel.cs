using HandyControl.Controls;
using LQClass.AdminForWPF.Infrastructure;
using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using LQClass.AdminForWPF.Models;
using LQClass.AdminForWPF.Views;
using Prism.Commands;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LQClass.AdminForWPF.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		#region ����

		private ObservableCollection<CustomMenuItem> _CustomMenus;
		public ObservableCollection<CustomMenuItem> CustomMenus { get { return _CustomMenus; } }

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
			_CustomMenus = windowModel.ReadCustomMenus();
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

		/// <summary>
		/// ѡ��˵�����
		/// </summary>
		private void RaiseSelectedItemHandler(CustomMenuItem menuItem)
		{
			var view= RegionManager.Regions[RegionNames.MainTabRegion].GetView("Home");
			//(view as TabItem).Header = menuItem.Name;
			RegionManager.Regions[RegionNames.MainTabRegion].Activate(view);
		}

		#endregion
	}
}
