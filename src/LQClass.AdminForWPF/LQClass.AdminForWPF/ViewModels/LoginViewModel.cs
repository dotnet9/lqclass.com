using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using HandyControl.Controls;
using HandyControl.Data;
using LQClass.AdminForWPF.ICommonService;
using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Models;
using LQClass.AdminForWPF.Infrastructure.Mvvm;
using LQClass.AdminForWPF.Infrastructure.Tools;
using LQClass.AdminForWPF.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Regions;

namespace LQClass.AdminForWPF.ViewModels;

public class LoginViewModel : ViewModelBase
{
    /// <summary>
    ///     切换语言
    /// </summary>
    /// <param name="loginModel"></param>
    public LoginViewModel(LoginModel loginModel,
        IRegionManager regionManager,
        ISystemMenuService systemMenuService) : base(regionManager)
    {
        ReadBackgroundImg();
        _loginModel = loginModel;
        _systemMenuService = systemMenuService;
        IsRemberMe = AppSettingsHelper.IsRemberMe;
        IsAutoLogin = AppSettingsHelper.IsAutoLogin;


        // 配置文件中的账号和密码为加密后的密文
        if (!string.IsNullOrWhiteSpace(AppSettingsHelper.UserName))
            UserName = DESHelper.Decrypt3Des(AppSettingsHelper.UserName);
        if (!string.IsNullOrWhiteSpace(AppSettingsHelper.Password))
            Password = DESHelper.Decrypt3Des(AppSettingsHelper.Password);

        if (IsAutoLogin) Task.FromResult(RaiseLogginHandler());
    }

    #region 私有方法

    private void ReadBackgroundImg()
    {
        try
        {
            var imgDir = $"{AppDomain.CurrentDomain.BaseDirectory}Images{Path.DirectorySeparatorChar}background";
            var childDirs = Directory.GetDirectories(imgDir);
            var rd = new Random(DateTime.Now.Millisecond);
            var chilDirIndex = rd.Next(childDirs.Length);
            var choidDir = childDirs[chilDirIndex];
            BackgroundImg = $"{choidDir}{Path.DirectorySeparatorChar}Frame.jpg";
        }
        catch (Exception ex)
        {
        }
    }

    #endregion

    #region 属性

    private string _BackgroundImg = string.Empty;

    /// <summary>
    ///     背景图片
    /// </summary>
    public string BackgroundImg
    {
        get => _BackgroundImg;
        set => SetProperty(ref _BackgroundImg, value);
    }

    private bool _isRemberMe;

    /// <summary>
    ///     是否记住我
    /// </summary>
    public bool IsRemberMe
    {
        get => _isRemberMe;
        set
        {
            SetProperty(ref _isRemberMe, value);
            if (value == false) IsAutoLogin = false;
        }
    }

    private bool _isAutoLogin;

    /// <summary>
    ///     是否自动登录
    /// </summary>
    public bool IsAutoLogin
    {
        get => _isAutoLogin;
        set
        {
            SetProperty(ref _isAutoLogin, value);
            if (value) IsRemberMe = true;
        }
    }

    private string _userName;

    /// <summary>
    ///     登录用户名
    /// </summary>
    public string UserName
    {
        get => _userName;
        set
        {
            SetProperty(ref _userName, value);
            (RaiseLoginCommand as DelegateCommand).RaiseCanExecuteChanged();
        }
    }

    private string _password;

    /// <summary>
    ///     登录密码
    /// </summary>
    public string Password
    {
        get => _password;
        set
        {
            SetProperty(ref _password, value);
            (RaiseLoginCommand as DelegateCommand).RaiseCanExecuteChanged();
        }
    }

    private bool _IsBusy;

    /// <summary>
    ///     是否正在繁忙
    /// </summary>
    public bool IsBusy
    {
        get => _IsBusy;
        set => SetProperty(ref _IsBusy, value);
    }


    /// <summary>
    ///     超链接列表
    /// </summary>
    public List<QuickLink> QuickLinks => QuickLinksSection.GetAllQuickLinks();

    #endregion

    #region 命令

    private ICommand _RaiseChangePasswordCommand;

    /// <summary>
    ///     输入密码命令
    /// </summary>
    public ICommand RaiseChangePasswordCommand =>
        _RaiseChangePasswordCommand ??
        (_RaiseChangePasswordCommand = new DelegateCommand<string>(pwd => Password = pwd));

    private ICommand _raiseLoginCommand;

    /// <summary>
    ///     登录命令
    /// </summary>
    public ICommand RaiseLoginCommand =>
        _raiseLoginCommand ?? (_raiseLoginCommand =
            new DelegateCommand(async () => await RaiseLogginHandler(), CanRaiseLoginCommand));

    #endregion

    #region 私有字段

    private readonly LoginModel _loginModel;
    private ISystemMenuService _systemMenuService;

    #endregion

    #region 命令处理方法

    public event Action<bool> LoginComplete;

    /// <summary>
    ///     处理登录操作
    /// </summary>
    private async Task RaiseLogginHandler()
    {
        try
        {
            if (IsIndeterminate) return;
            IsIndeterminate = true;

            // 使用cookie的方式登录，动态获取菜单信息
            //Password = (this.PasswordBox == null ? Password : this.PasswordBox.Password);
            var response = await _loginModel.Login(UserName, Password);
            if (response.IsSuccessful)
            {
                LoginResultDto.Instance = JsonConvert.DeserializeObject<LoginResultDto>(response.Content);
                AppSettingsHelper.IsRemberMe = IsRemberMe;
                AppSettingsHelper.IsAutoLogin = IsAutoLogin;
                if (AppSettingsHelper.IsRemberMe)
                {
                    AppSettingsHelper.UserName = DESHelper.Encrypt3Des(UserName);
                    AppSettingsHelper.Password = DESHelper.Encrypt3Des(Password);
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
                Growl.ErrorGlobal(new GrowlInfo
                    { Message = $"{JsonHelper.FormatJsonString(response.Content)}", WaitTime = 5 });
                ShowTipMsg($"{JsonHelper.FormatJsonString(response.Content)}");
                AppSettingsHelper.IsAutoLogin = IsAutoLogin = false;
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
    ///     获取jwttoken
    /// </summary>
    /// <returns></returns>
    private async Task GetJwtTokenInfo()
    {
        var response = await _loginModel.LoginJwt(new LoginJwtDto { Account = UserName, Password = Password });
        LoginJwtResultDto.Instance = JsonConvert.DeserializeObject<LoginJwtResultDto>(response.Content);
    }

    /// <summary>
    ///     登录操作是否可用
    /// </summary>
    /// <returns></returns>
    private bool CanRaiseLoginCommand()
    {
        return !string.IsNullOrWhiteSpace(UserName);
    }

    #endregion
}