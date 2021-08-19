using HandyControl.Controls;
using HandyControl.Data;
using LQClass.AdminForWPF.ICommonService;
using LQClass.AdminForWPF.ICommonService.Dtos;
using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using LQClass.AdminForWPF.Infrastructure.Tools;
using LQClass.AdminForWPF.Models;

using Newtonsoft.Json;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LQClass.AdminForWPF.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		#region 属性

		private string _BackgroundImg = string.Empty;
		/// <summary>
		/// 背景图片
		/// </summary>
		public string BackgroundImg
		{
			get { return _BackgroundImg; }
			set { this.SetProperty(ref _BackgroundImg, value); }
		}

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
		private bool _IsBusy;
		/// <summary>
		/// 是否正在繁忙
		/// </summary>
		public bool IsBusy
		{
			get { return _IsBusy; }
			set
			{
				this.SetProperty(ref _IsBusy, value);
			}
		}


		/// <summary>
		/// 超链接列表
		/// </summary>
		public List<QuickLink> QuickLinks { get { return QuickLinksSection.GetAllQuickLinks(); } }

		#endregion

		#region 命令

		private ICommand _RaiseChangePasswordCommand;
		/// <summary>
		/// 输入密码命令
		/// </summary>
		public ICommand RaiseChangePasswordCommand =>
		  _RaiseChangePasswordCommand ?? (_RaiseChangePasswordCommand = new DelegateCommand<string>(pwd => this.Password = pwd));

		private ICommand _raiseLoginCommand;
		/// <summary>
		/// 登录命令
		/// </summary>
		public ICommand RaiseLoginCommand =>
		  _raiseLoginCommand ?? (_raiseLoginCommand = new DelegateCommand(async () => await RaiseLogginHandler(), CanRaiseLoginCommand));


		#endregion

		#region 私有字段

		private LoginModel _loginModel;
		private ISystemMenuService _systemMenuService;

		#endregion

		/// <summary>
		/// 切换语言
		/// </summary>
		/// <param name="loginModel"></param>
		public LoginViewModel(LoginModel loginModel,
			IRegionManager regionManager,
			ISystemMenuService systemMenuService) : base(regionManager)
		{
			ReadBackgroundImg();
			this._loginModel = loginModel;
			this._systemMenuService = systemMenuService;
			this.IsRemberMe = AppSettingsHelper.IsRemberMe;
			this.IsAutoLogin = AppSettingsHelper.IsAutoLogin;


			// 配置文件中的账号和密码为加密后的密文
			if (!string.IsNullOrWhiteSpace(AppSettingsHelper.UserName))
			{
				this.UserName = DESHelper.Decrypt3Des(AppSettingsHelper.UserName);
			}
			if (!string.IsNullOrWhiteSpace(AppSettingsHelper.Password))
			{
				this.Password = DESHelper.Decrypt3Des(AppSettingsHelper.Password);
			}

			if (this.IsAutoLogin)
			{
				Task.FromResult(RaiseLogginHandler());
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
				if (IsIndeterminate)
				{
					return;
				}
				IsIndeterminate = true;

				// 使用cookie的方式登录，动态获取菜单信息
				//Password = (this.PasswordBox == null ? Password : this.PasswordBox.Password);
				var response = await _loginModel.Login(UserName, Password);
				if (response.IsSuccessful)
				{
					LoginResultDto.Instance = JsonConvert.DeserializeObject<LoginResultDto>(response.Content);
					AppSettingsHelper.IsRemberMe = this.IsRemberMe;
					AppSettingsHelper.IsAutoLogin = this.IsAutoLogin;
					if (AppSettingsHelper.IsRemberMe)
					{
						AppSettingsHelper.UserName = DESHelper.Encrypt3Des(this.UserName);
						AppSettingsHelper.Password = DESHelper.Encrypt3Des(this.Password);
					}
					else
					{
						AppSettingsHelper.UserName =
						AppSettingsHelper.Password = string.Empty;
					}
					// 使用jwttoken的方式登录，获取jwttoken，用于其他api调用使用
					await GetJwtTokenInfo();
					LoginComplete?.Invoke(true);
				}
				else
				{
					Growl.ErrorGlobal(new GrowlInfo { Message = $"{JsonHelper.FormatJsonString(response.Content)}", WaitTime = 5 });
					ShowTipMsg($"{JsonHelper.FormatJsonString(response.Content)}");
					AppSettingsHelper.IsAutoLogin = this.IsAutoLogin = false;
				}
			}
			catch (Exception ex)
			{
				ShowTipMsg(ex.Message);
			}
			finally
			{
				IsIndeterminate = false;
				IsBusy = false;
			}
		}

		/// <summary>
		/// 获取jwttoken
		/// </summary>
		/// <returns></returns>
		private async Task GetJwtTokenInfo()
		{
			var response = await _loginModel.LoginJwt(new LoginJwtDto { Account = UserName, Password = Password });
			LoginJwtResultDto.Instance = JsonConvert.DeserializeObject<LoginJwtResultDto>(response.Content);
		}

		/// <summary>
		/// 登录操作是否可用
		/// </summary>
		/// <returns></returns>
		private bool CanRaiseLoginCommand() => !string.IsNullOrWhiteSpace(UserName);

		#endregion

		#region 私有方法 

		private void ReadBackgroundImg()
		{
			try
			{
				var imgDir = $"{AppDomain.CurrentDomain.BaseDirectory}Images{Path.DirectorySeparatorChar}background";
				var childDirs = System.IO.Directory.GetDirectories(imgDir);
				Random rd = new Random(DateTime.Now.Millisecond);
				var chilDirIndex = rd.Next(childDirs.Length);
				var choidDir = childDirs[chilDirIndex];
				this.BackgroundImg = $"{choidDir}{Path.DirectorySeparatorChar}Frame.jpg";
			}
			catch (Exception ex)
			{

			}
		}

		#endregion
	}



}
