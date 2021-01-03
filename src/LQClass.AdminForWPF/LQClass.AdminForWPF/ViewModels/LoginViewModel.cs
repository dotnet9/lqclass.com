using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using LQClass.AdminForWPF.Infrastructure.Tools;
using LQClass.AdminForWPF.Models;
using LQClass.AdminForWPF.Views;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Unity;

namespace LQClass.AdminForWPF.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		#region 属性

		private bool _isRemberMe = false;
		/// <summary>
		/// 是否记住我
		/// </summary>
		public bool IsRemberMe
		{
			get { return _isRemberMe; }
			set
			{
				this.SetProperty(ref _isRemberMe, value);
				if (value == false)
				{
					IsAutoLogin = false;
				}
			}
		}

		private bool _isAutoLogin = false;
		/// <summary>
		/// 是否自动登录
		/// </summary>
		public bool IsAutoLogin
		{
			get { return _isAutoLogin; }
			set
			{
				this.SetProperty(ref _isAutoLogin, value);
				if (value == true)
				{
					IsRemberMe = true;
				}
			}
		}

		private string _userName;
		/// <summary>
		/// 登录用户名
		/// </summary>
		public string UserName
		{
			get { return _userName; }
			set
			{
				this.SetProperty(ref _userName, value);
				(RaiseLoginCommand as DelegateCommand).RaiseCanExecuteChanged();
			}
		}

		private string _password;
		/// <summary>
		/// 登录密码
		/// </summary>
		public string Password
		{
			get { return _password; }
			set
			{
				this.SetProperty(ref _password, value);
				(RaiseLoginCommand as DelegateCommand).RaiseCanExecuteChanged();
			}
		}

		private bool _IsIndeterminate = false;
		/// <summary>
		/// 登录时显示正在繁忙
		/// </summary>
		public bool IsIndeterminate
		{
			get { return _IsIndeterminate; }
			set { this.SetProperty(ref _IsIndeterminate, value); }
		}

		/// <summary>
		/// 超链接列表
		/// </summary>
		public List<QuickLink> QuickLinks { get { return AppConfig.Instance.QuickLinks; } }

		#endregion

		#region 命令

		private ICommand _raiseLoginCommand;
		/// <summary>
		/// 登录命令
		/// </summary>
		public ICommand RaiseLoginCommand =>
		  _raiseLoginCommand ?? (_raiseLoginCommand = new DelegateCommand(async () => await RaiseLogginHandler(), CanRaiseLoginCommand));


		#endregion

		#region 私有字段

		private LoginModel _loginModel;

		#endregion

		/// <summary>
		/// 切换语言
		/// </summary>
		/// <param name="loginModel"></param>
		public LoginViewModel(LoginModel loginModel, IRegionManager regionManager) : base(regionManager)
		{
			this._loginModel = loginModel;
			this.IsRemberMe = AppConfig.Instance.IsRemberMe;
			this.IsAutoLogin = AppConfig.Instance.IsAutoLogin;


			// 配置文件中的账号和密码为加密后的密文
			if (!string.IsNullOrWhiteSpace(AppConfig.Instance.UserName))
			{
				this.UserName = DESHelper.Decrypt3Des(AppConfig.Instance.UserName);
			}
			if (!string.IsNullOrWhiteSpace(AppConfig.Instance.Password))
			{
				this.Password = DESHelper.Decrypt3Des(AppConfig.Instance.Password);
			}

			if (this.IsAutoLogin)
			{
				RaiseLogginHandler();
			}
		}

		#region 命令处理方法
		public event Action<bool> LoginComplete;
		/// <summary>
		/// 处理登录操作
		/// </summary>
		private async Task RaiseLogginHandler()
		{
			try
			{
				IsIndeterminate = true;
				var response = await _loginModel.Login(UserName, Password);
				if (response.IsSuccessful)
				{
					LoginResultDto.Instance = JsonConvert.DeserializeObject<LoginResultDto>(response.Content);
					AppConfig.Instance.IsRemberMe = this.IsRemberMe;
					AppConfig.Instance.IsAutoLogin = this.IsAutoLogin;
					if (AppConfig.Instance.IsRemberMe)
					{
						AppConfig.Instance.UserName = DESHelper.Encrypt3Des(this.UserName);
						AppConfig.Instance.Password = DESHelper.Encrypt3Des(this.Password);
					}
					else
					{
						AppConfig.Instance.UserName =
						  AppConfig.Instance.Password = string.Empty;
					}
					AppConfig.Instance.Save();
					LoginComplete?.Invoke(true);
				}
				else
				{
					ShowTipMsg($"{JsonHelper.FormatJsonString(response.Content)}");
					AppConfig.Instance.IsAutoLogin = this.IsAutoLogin = false;
					AppConfig.Instance.Save();
				}
			}
			catch (Exception ex)
			{
				ShowTipMsg(ex.Message);
			}
			finally
			{
				IsIndeterminate = false;
			}
		}

		/// <summary>
		/// 登录操作是否可用
		/// </summary>
		/// <returns></returns>
		private bool CanRaiseLoginCommand() => !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);

		#endregion


	}
}
