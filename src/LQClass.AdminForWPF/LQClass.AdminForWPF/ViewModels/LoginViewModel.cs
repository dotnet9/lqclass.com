using LQClass.AdminForWPF.Infrastructure.Configs;
using LQClass.AdminForWPF.Infrastructure.Tools;
using LQClass.AdminForWPF.Views;
using Prism.Commands;
using Prism.Mvvm;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LQClass.AdminForWPF.ViewModels
{
  public class LoginViewModel : BindableBase
  {
    #region 属性

    /// <summary>
    /// 窗体标题
    /// </summary>
    public string Title
    {
      get { return AppConfig.Instance.Name; }
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
      _raiseLoginCommand ?? (_raiseLoginCommand = new DelegateCommand(RaiseLogginHandler, CanRaiseLoginCommand));

    private ICommand _raiseOpenLinkCommand;
    /// <summary>
    /// 打开命令
    /// </summary>
    public ICommand RaiseOpenLinkCommand =>
      _raiseOpenLinkCommand ?? (_raiseOpenLinkCommand = new DelegateCommand<string>(RaiseOpenLinkHandler));

    #endregion

    public LoginViewModel()
    {
      this.IsRemberMe = AppConfig.Instance.IsRemberMe;
      this.IsAutoLogin = AppConfig.Instance.IsAutoLogin;
      this.UserName = AppConfig.Instance.UserName;
      this.Password = AppConfig.Instance.Password;

      if (this.IsAutoLogin)
      {
        RaiseLogginHandler();
      }
    }

    #region 命令处理方法

    /// <summary>
    /// 处理登录操作
    /// </summary>
    private async void RaiseLogginHandler()
    {
      try
      {
        var response = await Login(AppConfig.Instance.RunningConfig.API, UserName, Password);
        if (response.IsSuccessful)
        {
          AppConfig.Instance.IsRemberMe = this.IsRemberMe;
          AppConfig.Instance.IsAutoLogin = this.IsAutoLogin;
          if (AppConfig.Instance.IsRemberMe)
          {
            AppConfig.Instance.UserName = DESHelper.Encrypt3Des(this.UserName);
            AppConfig.Instance.Password = DESHelper.Encrypt3Des(this.Password);
          }
          AppConfig.Instance.Save();
          MessageBox.Show($"登录成功：{JsonHelper.FormatJsonString(response.Content)}");
          var win = App.Current.MainWindow;
          new MainWindowView().Show();
          win.Close();
        }
        else
        {
          MessageBox.Show($"登录失败：{JsonHelper.FormatJsonString(response.Content)}");
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
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

    private async Task<IRestResponse> Login(string apiAddress, string userName, string pwd)
    {
      var client = new RestClient($"{apiAddress}_login/Login");
      client.Timeout = -1;
      var request = new RestRequest(Method.POST);
      request.AlwaysMultipartFormData = true;
      request.AddParameter("userid", userName);
      request.AddParameter("password", pwd);
      request.AddParameter("cookie", "false");
      var response = await client.ExecuteAsync(request);

      return response;
    }

    /// <summary>
    /// 登录操作是否可用
    /// </summary>
    /// <returns></returns>
    private bool CanRaiseLoginCommand() => !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);

    #endregion
  }
}
