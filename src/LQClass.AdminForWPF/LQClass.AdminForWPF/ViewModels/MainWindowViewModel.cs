using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using LQClass.AdminForWPF.Models;
using LQClass.AdminForWPF.Views;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LQClass.AdminForWPF.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		#region 属性

		private ObservableCollection<CustomMenu> _CustomMenus;
		public ObservableCollection<CustomMenu> CustomMenus { get { return _CustomMenus; } }

		#endregion

		#region 命令


		private ICommand _RaiseLogoutCommand;
		/// <summary>
		/// 用户命令
		/// </summary>
		public ICommand RaiseLogoutCommand =>
		  _RaiseLogoutCommand ?? (_RaiseLogoutCommand = new DelegateCommand(RaiseLogoutHandler));

		#endregion

		#region 私有变量

		private MainWindowModel windowModel = null;

		#endregion

		public MainWindowViewModel(MainWindowModel mainWindowModel)
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

		#endregion
	}
}
