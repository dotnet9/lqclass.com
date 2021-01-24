using HandyControl.Controls;
using HandyControl.Data;
using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Windows.Input;

namespace LQClass.AdminForWPF.Infrastructure.Mvvm
{
	public class ViewModelBase : BindableBase
	{
		#region 共用属性

		private bool _IsIndeterminate = false;
		/// <summary>
		/// 登录时显示正在繁忙
		/// </summary>
		public bool IsIndeterminate
		{
			get { return _IsIndeterminate; }
			set { this.SetProperty(ref _IsIndeterminate, value); }
		}

		private string _CurrentLanguage;
		/// <summary>
		/// 当前选择的选择
		/// </summary>
		public string CurrentLanguage
		{
			get { return _CurrentLanguage; }
			set
			{
				this.SetProperty(ref _CurrentLanguage, value);
			}
		}

		/// <summary>
		/// 语言列表
		/// </summary>
		public List<LanguageModel> Languages { get { return LanguageModel.GetLanguages(); } }

		/// <summary>
		/// 提示控件，未找到设置时间等绑定属性，先由view输入viewmodel
		/// </summary>
		//public static Snackbar Snackbar { get; set; }

		#endregion

		#region 共用命令

		private ICommand _RaiseShowDialogCommand;
		/// <summary>
		/// 打开对话框
		/// </summary>
		public ICommand RaiseShowDialogCommand =>
		  _RaiseShowDialogCommand ?? (_RaiseShowDialogCommand = new DelegateCommand<string>(RaiseShowDialogHandler));

		#endregion

		#region private变量

		public readonly IRegionManager RegionManager;
		public readonly IModuleManager ModuleManager;
		public readonly IDialogService DialogService;
		public readonly IEventAggregator EventAggregator;

		#endregion

		public ViewModelBase(IRegionManager regionManager) : this()
		{
			RegionManager = regionManager;
		}
		public ViewModelBase(IRegionManager regionManager, 
			IModuleManager moduleManager,
			IDialogService dialogService,
			IEventAggregator eventAggregator) : this()
		{
			RegionManager = regionManager;
			ModuleManager = moduleManager;
			DialogService = dialogService;
			EventAggregator = eventAggregator;
			ModuleManager.LoadModuleCompleted += _moduleManager_LoadModuleCompleted;
		}

		public ViewModelBase()
		{
		}
		private void _moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
		{
			//DialogService.ShowDialog("SuccessDialog", new DialogParameters($"message={e.ModuleInfo.ModuleName + "模块被加载了"}"), null);
		}

		#region 命令处理方法

		/// <summary>
		/// 显示对话框
		/// </summary>
		/// <param name="dlgName"></param>
		private void RaiseShowDialogHandler(string dlgName)
		{
			DialogService.ShowDialog(dlgName);
		}

		#endregion



		/// <summary>
		/// 显示界面提示信息
		/// </summary>
		/// <param name="msg"></param>
		public void ShowTipMsg(string msg)
		{
			Growl.ErrorGlobal(new GrowlInfo { Message = msg, WaitTime = 5 });
			//  Snackbar?.MessageQueue?.Enqueue(msg,
			//null,
			//null,
			//null,
			//false,
			//true,
			//TimeSpan.FromSeconds(3));
		}

		public void CurrentUICultureChanged()
		{

		}
	}
}
