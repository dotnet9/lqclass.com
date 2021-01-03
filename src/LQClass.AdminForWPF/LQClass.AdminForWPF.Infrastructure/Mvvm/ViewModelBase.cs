using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace LQClass.AdminForWPF.Infrastructure.Mvvm
{
	public class ViewModelBase : BindableBase
	{
		#region 共用属性

		/// <summary>
		/// 窗体标题
		/// </summary>
		public string Title
		{
			get { return AppConfig.Instance.Name; }
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
		public Snackbar Snackbar { get; set; }

		#endregion

		#region 共用命令


		private ICommand _RaiseChangeLanguageCommand;
		/// <summary>
		/// 切换语言命令
		/// </summary>
		public ICommand RaiseChangeLanguageCommand =>
		  _RaiseChangeLanguageCommand ?? (_RaiseChangeLanguageCommand = new DelegateCommand<string>(RaiseChangeLanguageHandler));

		private ICommand _raiseOpenLinkCommand;
		/// <summary>
		/// 打开命令
		/// </summary>
		public ICommand RaiseOpenLinkCommand =>
		  _raiseOpenLinkCommand ?? (_raiseOpenLinkCommand = new DelegateCommand<string>(RaiseOpenLinkHandler));

		#endregion

		#region private变量

		public readonly IRegionManager RegionManager;

		#endregion

		public ViewModelBase(IRegionManager regionManager)
		{
			RegionManager = regionManager;
			RaiseChangeLanguageHandler(AppConfig.Instance.Language);
		}

		#region 命令处理方法

		/// <summary>
		/// 切换语言
		/// </summary>
		/// <param name="languageKey"></param>
		private void RaiseChangeLanguageHandler(string languageKey)
		{
			var lan = Languages.Find(cu => cu.Key == languageKey);
			this.CurrentLanguage = lan.Name;
			AppConfig.Instance.SetLanguage(languageKey);
		}

		/// <summary>
		/// 打开超链接
		/// </summary>
		private void RaiseOpenLinkHandler(string url)
		{
			Process.Start(new ProcessStartInfo("cmd", $"/c start {url}")
			{
				UseShellExecute = false,
				CreateNoWindow = true
			});
		}

		#endregion

		/// <summary>
		/// 显示界面提示信息
		/// </summary>
		/// <param name="msg"></param>
		public void ShowTipMsg(string msg)
		{
			Snackbar.MessageQueue?.Enqueue(msg,
				null,
				null,
				null,
				false,
				true,
				TimeSpan.FromSeconds(3));
		}
	}
}
